using EShopMVCNet7.Areas.Admin.ViewModels.Product;
using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        public ProductController(EshopDbContext db) : base(db)
        {
        }

        public IActionResult Index(int page = 1)
        {
            var products = _db.AppProducts
                                .Select(p => new ProductListitemVM
                                {
                                    Id = p.Id,
                                    Name = p.Name,
                                    CategoryId = p.CategoryId,
                                    CoverImg = p.CoverImg,
                                    InStock = p.InStock,
                                    DiscountFrom = p.DiscountFrom,
                                    DiscountPrice = p.DiscountPrice,
                                    DiscountTo = p.DiscountTo,
                                    Price = p.Price,
                                    View = p.View,
                                    CategoryName = p.Category.Name,
                                })
                                .OrderByDescending(p => p.Id)
                                .ToPagedList(page, PER_PAGE);
            return View(products);
        }

        public IActionResult Create()
        {
            //lấy dữ liệu từ database
            var cate = _db.AppCategories
                                .OrderByDescending(c => c.Id)
                                .ToList();
            //ép kiểu để sử dụng được với asp-items
            ViewBag.Category = new SelectList(cate, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductUpdinVM productVM, [FromServices] IWebHostEnvironment env)
        {
            //Xác thực dữ liệu
            if (ModelState.IsValid == false)
            {
                return View(productVM);
            }
            //sao chép dữ liệu từ ViewModel sang model
            var product = new AppProduct
            {
                Name = productVM.Name,
                Slug = productVM.Slug,
                Summary = productVM.Summary,
                Content = productVM.Content,
                InStock = productVM.InStock,
                Price = productVM.Price,
                DiscountPrice = productVM.DiscountPrice,
                DiscountFrom = productVM.DiscountFrom,
                DiscountTo = productVM.DiscountTo,
                CategoryId = productVM.CategoryId,
                View = 0,
                CreatedAt = DateTime.Now,
            };

            //hình ảnh bìa(CoverImg)
            product.CoverImg = UploadFile(productVM.CoverImg, env.WebRootPath);

            //upload ảnh sản phẩm(nhiều ảnh)
            foreach (var img in productVM.ProductImages)
            {
                if (img is not null)
                {
                    var productImg = new AppProductImage();
                    productImg.Path = UploadFile(img, env.WebRootPath);
                    product.ProductImages.Add(productImg);
                }
            }

            try
            {
                _db.Add(product);
                _db.SaveChanges();
                SetSuccessMesg("Thêm sản phẩm thành cồng");
            }
            catch (Exception ex)
            {
                SetErrorMesg(ex.Message);
            }
            return RedirectToAction(nameof(Create));
        }
        
        public IActionResult Update(int id)
        {
            var data = _db.AppProducts
                .Select(p => new ProductUpdinVM
                {
                    CategoryId = p.CategoryId.GetValueOrDefault(0),
                    Id = p.Id,
                    Content = p.Content,
                    DiscountFrom = p.DiscountFrom,
                    DiscountTo= p.DiscountTo,
                    DiscountPrice= p.DiscountPrice,
                    InStock= p.InStock,
                    Name= p.Name,
                    Price= p.Price,
                    Slug= p.Slug,
                    Summary= p.Summary,
                    CoverImgPath = p.CoverImg,
                    //Lấy dữ liệu Path từ ảnh sản phẩm cho vào list
                    ProductImgPath = p.ProductImages.Select(pi => pi.Path).ToList()
                })
                .Where(p => p.Id == id).SingleOrDefault();

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(Index));
            }

            //lấy dữ liệu category từ database
            var cate = _db.AppCategories
                                .OrderByDescending(c => c.Id)
                                .ToList();
            //ép kiểu để sử dụng được với asp-items
            ViewBag.Category = new SelectList(cate, "Id", "Name", data.CategoryId);
            return View(data);
        }
        public IActionResult Delete(int id, [FromServices] IWebHostEnvironment env)
        {
            var data = _db.AppProducts.Find(id);

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sản phẩm");
                return RedirectToAction(nameof(Index));
            }
            //lấy ảnh của sản phẩm bị xóa
            var listImgs = _db.AppProductImages
                                .Where(x => x.ProductId == id)
                                .ToList();
            try
            {
            //Xóa dữ liệu trong db
            _db.Remove(data);
                //xóa ảnh sản phẩm trong disk
                //ảnh cover
                System.IO.File.Delete(Path.Combine(env.WebRootPath, data.CoverImg.TrimStart('/')));
                //ảnh chi tiết
                foreach(var img in listImgs)
                {
                    System.IO.File.Delete(Path.Combine(env.WebRootPath, img.Path.TrimStart('/')));
                }

                SetSuccessMesg($"Xóa sản phẩm [{data.Name}] thành công!");
            }catch (Exception ex)
            {
                SetErrorMesg("Xóa không được Chi tiết: " + ex.Message);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Upload và trả về file name, file được lưu trong thư mục upload
        /// </summary>
        /// <param name="file"></param>
        /// <param name="dir"></param>
        /// <returns>Filename</returns>
        private string UploadFile(IFormFile file, string dir)
        {
            var fName = file.FileName;
            fName = Path.GetFileNameWithoutExtension(fName)
                + DateTime.Now.Ticks
                + Path.GetExtension(fName);
            //gán giá trị cho cột CoverImg
            var res = "/upload/" + fName;
            //Tạo đường dẫn tuyệt đối( vd: E:/Project/wwwroot/upload/xxx.jpg
            fName = Path.Combine(dir, "upload", fName);
            //Tạo stream để lưu file
            var stream = System.IO.File.Create(fName);
            file.CopyTo(stream);
            stream.Dispose(); //giải phóng bộ nhớ

            return res;
        }
    }
}
