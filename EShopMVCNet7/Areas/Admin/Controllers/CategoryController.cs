using EShopMVCNet7.Areas.Admin.ViewModels.Category;
using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        public CategoryController(EshopDbContext db) : base(db)
        {
        }
        public IActionResult Index() => View();

        //cách 1
        public IActionResult ListAll2()
        {
            var data = _db.AppCategories
                .OrderByDescending(x => x.Id)
                .ToList();
            return Ok(data);
        }

        //cách 2
        public List<CategoryListitemVM> ListAll()
        {
            var data = _db.AppCategories
                      .Select(x => new CategoryListitemVM
                      {
                          Id = x.Id,
                          Name = x.Name,
                      })
                      .OrderByDescending(x => x.Id)
                      .ToList();
            return data;
        }
    }
}
