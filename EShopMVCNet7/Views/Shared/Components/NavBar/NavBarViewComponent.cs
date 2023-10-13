using EShopMVCNet7.Areas.Admin.ViewModels.Category;
using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVCNet7.Views.Shared.Components.NavBar
{
    public class NavBarViewComponent : ViewComponent
    {
        private EshopDbContext _db;

        public NavBarViewComponent(EshopDbContext db)//contractor cần cùng tên với class
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var category = _db.AppCategories
                .OrderByDescending(c => c.Id)
                .ToList();
            return View(category);
        }
    }
}
