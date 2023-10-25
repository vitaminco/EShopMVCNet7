using EShopMVCNet7.Common;
using EShopMVCNet7.Models;
using EShopMVCNet7.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVCNet7.Controllers
{
    public class AccountController : ClientBaseController
    {
        public AccountController(EshopDbContext db):base(db)
        {            
        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(AppUser user)
        {
                if (ModelState.IsValid == false)
                {
                    return View(user);
                }
                //chuẩn hóa username và email
                user.Username = user.Username.ToLower().Trim();
                user.Email = user.Email.ToLower().Trim();
                //check username và email đã tồn tại chưa
                var exists = _db.AppUsers.Any(u => u.Email == user.Email || u.Username == user.Username);
                if (exists)
                {
                    ModelState.AddModelError("", "Email hoặc tên đăng nhập đã được sử dụng!");
                    return View(user);
                }
                //hash mật khẩu
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                //khai báo biến sử lý data

                user.Role = UserRole.ROLECUSTOMER;
                user.BlockedTo = null;

                _db.AppUsers.Add(user);
                _db.SaveChanges();
            
            return RedirectToAction(nameof(Register));//nameof là để ở trên đổi tên thì dưới nó cx đổi theo
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if(ModelState.IsValid == false)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ");
                return View();
            }
            var user = _db.AppUsers.SingleOrDefault(u => u.Username == loginVM.Username);
            //check user
            if(user == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ!!!");
                return View();
            }
            //check mật khẩu
            if(BCrypt.Net.BCrypt.Verify(loginVM.Password, user.Password) == false)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không hợp lệ!!!");
                return View();
            }

            HttpContext.SetUserId(user.Id);
            HttpContext.SetUserName(user.Username);
            HttpContext.SetRole(user.Role);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
