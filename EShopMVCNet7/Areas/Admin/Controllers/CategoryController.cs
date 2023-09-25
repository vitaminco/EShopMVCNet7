using EShopMVCNet7.Areas.Admin.ViewModels.Category;
using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        public CategoryController(EshopDbContext db) : base(db)
        {
        }
        public IActionResult Index() => View();

        //cách 1
        /*public IActionResult ListAll2()
        {
            var data = _db.AppCategories
                .OrderByDescending(x => x.Id)
                .ToList();
            return Ok(data);
        }*/

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
        //Admin/Category/UpSert/1
        public IActionResult UpSert(int? id, [FromBody] CategoryUpdinVM item)
        {
            if(id == null)
            {
                //copy dữ liệu từ view model sang model
                var category = new AppCategory
                {
                    Name = item.Name,
                    Slug = item.Slug,
                };
                _db.Add(category);
                _db.SaveChanges();
                return Ok($"Thêm danh mục [{item.Name}] thành công");
            }
            else
            {
                //cập nhật dữ liệu
                var oldCategory = _db.AppCategories.Find(id);
                if(oldCategory != null)
                {
                    oldCategory.Name = item.Name;
                    oldCategory.Slug = item.Slug;

                    _db.SaveChanges();
                }
            }
            return Ok($"Cập nhật danh mục [{item.Name}] thành công");
        }
        //cập nhật
        public AppCategory Detail(int id)
        {
            return _db.AppCategories.Find(id);
        }
        //xóa
        public IActionResult Delete(int id)
        {
            var data = _db.AppCategories.Find(id);

            if (data != null)
            {
                _db.Remove(data);
                _db.SaveChanges(true);
            }
           
            return Ok("Xóa danh mục thành công");
        }
    }
}
