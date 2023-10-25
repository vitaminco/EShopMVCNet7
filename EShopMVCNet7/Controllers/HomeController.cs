using EShopMVCNet7.Models;
using EShopMVCNet7.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace EShopMVCNet7.Controllers
{
    public class HomeController : ClientBaseController
    {
        public HomeController(EshopDbContext db) : base(db)
        {
        }


        [Route("/san-pham/{slug?}/{cateId:int?}")]
        [Route("/")] //trang chủ
        public IActionResult Index(int page = 1, int? cateId = null, string? slug = "")
        {
            ViewBag.Title = "Trang chủ";
            var query = _db.AppProducts.AsQueryable();

            if (cateId != null)
            {
                var cateName = _db.AppCategories.Find(cateId)?.Name;
                ViewBag.Title = "Sản phẩm " + cateName;
                query = query.Where(p => p.CategoryId == cateId);
            }
            var data = query.Select(p => new ProductListItemVM
            {
                Id = p.Id,
                Name = p.Name,
                CoverImg = p.CoverImg,
                Price = p.Price,
                DiscountFrom = p.DiscountFrom,
                DiscountPrice = p.DiscountPrice,
                DiscountTo = p.DiscountTo,
                Summary = p.Summary,
                CategoryName = p.Category.Name,
            })
                            .OrderByDescending(p => p.Id)
                            .ToPagedList(page, PER_PAGE);
            return View(data);
        }
        [Route("/tim-kiem")]
        public IActionResult Search(int page = 1, string keyword = "")
        {
            ViewBag.Title =$"Kết quả tìm kiếm cho '{keyword}'";
            var data = _db.AppProducts.Where(p => p.Name.Contains(keyword) || p.Summary.Contains(keyword))
                                      .Select(p => new ProductListItemVM
                                      {
                                          Id = p.Id,
                                          Name = p.Name,
                                          CoverImg = p.CoverImg,
                                          Price = p.Price,
                                          DiscountFrom = p.DiscountFrom,
                                          DiscountPrice = p.DiscountPrice,
                                          DiscountTo = p.DiscountTo,
                                          Summary = p.Summary,
                                          CategoryName = p.Category.Name,
                                      }).OrderByDescending(p => p.Id)
                                        .ToPagedList(page, PER_PAGE);
            return View("Index",data);
        }
        public IActionResult DetailProducts(int id)
        {
            //Load ảnh sản phẩm và chi tiết sản phẩm
            var data = _db.AppProducts
                           .Include(p => p.ProductImages)
                           .Where(p =>p.Id == id)
                           .SingleOrDefault();
            if(data == null)
            {
                SetErrorMesg("Không tìm thấy");
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
    }
}