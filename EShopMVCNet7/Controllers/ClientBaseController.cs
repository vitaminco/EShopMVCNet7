using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVCNet7.Controllers
{
    public class ClientBaseController : Controller
    {
        protected const int PER_PAGE = 20;
        protected EshopDbContext _db;

        public ClientBaseController(EshopDbContext db)
        {
            _db = db;
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
