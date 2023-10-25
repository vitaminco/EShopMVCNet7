using EShopMVCNet7.Common;
using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        protected const int PER_PAGE = 20;
        protected EshopDbContext _db;

        public AdminBaseController(EshopDbContext db)
        {
            _db = db;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.GetUserId() == null)
            {
                context.Result = new RedirectToActionResult("Login","Account", new {area = ""});
                SetErrorMesg("Chưa đăng nhập!! vui lòng đăng nhập để sử dụng chức năng này");
                return;
            }
            //Check quyền Admin
            if(context.HttpContext.IsAdmin() == false)
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { area = "" });
                SetErrorMesg("Bạn không có quyền này!!!");
                return;
            }
        }
        protected void SetSuccessMesg(string msg)
        {
            TempData["_SuccessMesg"] = msg;
        }
        protected void SetErrorMesg(string msg)
        {
            TempData["_ErrorMesg"] = msg;
        }
    }
}
