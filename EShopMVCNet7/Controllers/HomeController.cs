﻿using EShopMVCNet7.Models;
using EShopMVCNet7.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace EShopMVCNet7.Controllers
{
    public class HomeController : ClientBaseController
    {
        public HomeController(EshopDbContext db) : base(db)
        {
        }


        [Route("/{slug?}/{cateId:int?}")]
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
    }
}