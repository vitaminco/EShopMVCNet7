using EShopMVCNet7.Models;
using EShopMVCNet7.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVCNet7.Controllers
{
    public class CheckOutController : ClientBaseController
    {
        public CheckOutController(EshopDbContext db) : base(db) { }
        [HttpPost]
        public IActionResult CheckOut(CustomerInfoVM customer)
        {
            if (ModelState.IsValid == false)
            {
                SetErrorMesg("Dữ liệu không hợp lệ.");
            }
            AppOrder order = new()
            {
                CustomerAddress = customer.CustomerAddress,
                CustomerEmail = customer.CustomerEmail,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                Status = Common.OrderStatus.PENDING,
                CreatedAt = DateTime.Now
            };
            //Trích xuất thông tin từ giỏ hàng
            var cartIds = HttpContext.Session.Keys
                .Where(c => c.StartsWith("Cart_"))
                .Select(c => Convert.ToInt32(c.Substring(5)))
                .ToList();
            if (cartIds != null)
            {
                //lấy thông tin sản phẩm
                var dataProducts = _db.AppProducts.Where(p => cartIds.Contains(p.Id))
                    .Select(p => new CartListItemVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CoverImg = p.CoverImg,
                        Price = p.Price,
                        DiscountFrom = p.DiscountFrom,
                        DiscountPrice = p.DiscountPrice,
                        DiscountTo = p.DiscountTo,
                        QuantityInCart = HttpContext.Session.GetInt32("Cart_" + p.Id) ?? 0,
                    })
                    .ToList();
                //Thêm sản phẩm vào order detail
                foreach (var p in dataProducts)
                {
                    order.Detail.Add(new AppOrderDetal
                    {
                        Price = p.Price,
                        ProductId = p.Id,
                        ProductName = p.Name,
                        Quantity = p.QuantityInCart
                    });
                }
                //Tính tổng giá
                order.TotalPrice = order.Detail.Sum(o => o.Quantity * o.Price).Value;
                //lưu vào db
                _db.Add(order);
                _db.SaveChanges();
                SetSuccessMesg("Đơn hàng đã được đặt hàng thành công");
            }
                return RedirectToAction("Index", "Home");/*Trang nào , controller nào*/
        }
    }
}
