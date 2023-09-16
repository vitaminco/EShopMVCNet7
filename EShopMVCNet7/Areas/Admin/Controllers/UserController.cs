using EShopMVCNet7.Common;
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
        [HttpGet]

        //phân trang
        public IActionResult Index(int page = 1)
        {
            var data = _db.AppUsers
                .OrderByDescending(x => x.Id)
                .ToPagedList(page, PER_PAGE);//được viết in hoa;;; phân trang all page
            return View(data);//chuyển data qua view
        }
        public IActionResult Create() => View();
        //đăng kí
        [HttpPost]
        public IActionResult Create(AppUser user, [FromServices] EshopDbContext db)
        {
            if (ModelState.IsValid == false)
            {
                return View(user);
            }
            //chuẩn hóa username và email 
            user.Username = user.Username.ToLower().Trim();
            user.Email = user.Email.ToLower().Trim();
            //check username và email đã tồn tại chưa
            var exists = db.AppUsers.Any(u => u.Email == user.Email || u.Username == user.Username);
            if (exists)
            {
                ModelState.AddModelError("", "Email hoặc tên đăng nhập đã được sử dụng!");
                return View(user);
            }
            //hash mật khẩu
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            //khai báo biến sử lý data

            /*user.Role = UserRole.ROLECUSTOMER;*/
            user.Role = user.Role;
            user.BlockedTo = null;

            db.AppUsers.Add(user);
            db.SaveChanges();

            return RedirectToAction(nameof(Create));//nameof là để ở trên đổi tên thì dưới nó cx đổi theo
        }
        //update
        public IActionResult Update(int id)
        {
            var data = _db.AppUsers.Find(id);

            if (data == null)
            {
                SetErrorMesg("Thông tin tài khoản không hợp lệ");
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(int id, AppUser user)
        {
            //tìm
            var oldUser = _db.AppUsers.Find(id);

            if (oldUser == null)
            {
                return RedirectToAction(nameof(Index));
            }
            // bỏ qua validate cho một số thuộc tính
            ModelState.Remove("Username");
            ModelState.Remove("Password");
            ModelState.Remove("Cfm_Password");
            //calidate data
            if (ModelState.IsValid == false)
            {
                return View(user);
            }
            //chuẩn hóa Email
            user.Email = user.Email.ToLower().Trim();
            //check Email đã tồn tại chưa
            var exists = _db.AppUsers.Any(u => u.Email == user.Email && u.Id != id);
            if (exists)
            {
                ModelState.AddModelError("", "Email hoặc tên đăng nhập đã được sử dụng!");
                return View(user);
            }
            //update
            oldUser.Role = user.Role;
            oldUser.Email = user.Email;
            oldUser.Address = user.Address;
            oldUser.Phone = user.Phone;
            //save
            _db.SaveChanges();
            SetSuccessMesg("Cập nhật thành công!!");

            return RedirectToAction(nameof(Index));
        }
        //xóa
        public IActionResult Delete(int id)
        {
            var data = _db.AppUsers.Find(id);

            if (data == null)
            {
                return RedirectToAction(nameof(Index));
            }
            _db.Remove(data);
            _db.SaveChanges();

            SetSuccessMesg("Xóa thành công!");

            return RedirectToAction(nameof(Index));
        }
    }
}
