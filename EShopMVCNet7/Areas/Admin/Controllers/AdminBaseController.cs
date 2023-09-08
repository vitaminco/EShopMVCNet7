using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
