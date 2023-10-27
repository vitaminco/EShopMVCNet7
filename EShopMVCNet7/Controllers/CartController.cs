using EShopMVCNet7.Models;
using EShopMVCNet7.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EShopMVCNet7.Controllers
{
    public class CartController : ClientBaseController
    {
        public CartController(EshopDbContext db) : base(db)
        {
        }
        public IActionResult Index()
        {
            var cartIds = HttpContext.Session.Keys
                .Where(c => c.StartsWith("Cart_"))
                .Select(c =>Convert.ToInt32(c.Substring(5)))
                .ToList();
            if(cartIds != null)
            {
                //lấy thông tin sản phẩm
                var dataProducts = _db.AppProducts.Where(p=>cartIds.Contains(p.Id))
                    .Select(p => new CartListItemVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CoverImg = p.CoverImg,
                        Price = p.Price,
                        DiscountFrom = p.DiscountFrom,
                        DiscountPrice = p.DiscountPrice,
                        DiscountTo = p.DiscountTo,
                        QuantityInCart = HttpContext.Session.GetInt32("Cart_" + p.Id)??0,
                    })
                    .ToList();
                return View(dataProducts);
            }
            return View();
        }
        public IActionResult AddToCart([FromQuery] int productId)
        {
            //chưa có sản phẩm
            var quantity = HttpContext.Session.GetInt32("Cart_" + productId) ?? 0;
            HttpContext.Session.SetInt32("Cart_" + productId, quantity + 1);

            var referer = HttpContext.Request.Headers["referer"].ToString();
            SetSuccessMesg("Thêm vào giỏ hàng thành công");
            return Redirect(referer);
        }
        //xóa sp trong cart
        public IActionResult RemoveProduct([FromQuery] int productId)
        {
            HttpContext.Session.Remove("Cart_" + productId);
            SetSuccessMesg("Xóa thành công");
            return RedirectToAction(nameof(Index));
        }
    }
}
