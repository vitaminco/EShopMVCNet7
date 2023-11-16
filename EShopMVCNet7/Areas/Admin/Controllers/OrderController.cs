using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    public class OrderController : AdminBaseController
    {
        public OrderController(EshopDbContext db) : base(db)
        {
        }
        public IActionResult Index(int page = 1)
        {

            var data = _db.AppOrders
                .OrderByDescending(o => o.Id)
                .ToPagedList(page, PER_PAGE);
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var data = _db.AppOrders.Find(id);

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(Index));
            }
            //lấy ảnh của sản phẩm bị xóa
            var DetailOrder = _db.AppOrderDetals
                                .Where(x => x.OrderId == id)
                                .ToList();
            try
            {
                //Xóa dữ liệu trong db
                _db.Remove(data);
                //ảnh chi tiết
                foreach (var o in DetailOrder)
                {
                    _db.Remove(data);
                }

                SetSuccessMesg($"Xóa [{data.CustomerName}] thành công!");
            }
            catch (Exception ex)
            {
                SetErrorMesg("Xóa không được Chi tiết: " + ex.Message);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
