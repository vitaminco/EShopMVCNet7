using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    //kế thừa của adminbasecontroller
    public class UserController : AdminBaseController
    {
        public UserController(EshopDbContext db) : base(db)
        {
        }

        //phân trang
        public IActionResult Index(int page = 1)
        {
            var data = _db.AppUsers
                .OrderByDescending(x => x.Id)
                .ToPagedList(page, PER_PAGE);//được viết in hoa;;; phân trang all page
            return View(data);//chuyển data qua view
        }
        //update
        public IActionResult Update(int id)
        {
            var data = _db.AppUsers.Find(id);

            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
    }
}
