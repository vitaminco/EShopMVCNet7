using EShopMVCNet7.Areas.Admin.ViewModels.Event;
using EShopMVCNet7.Areas.Admin.ViewModels.Product;
using EShopMVCNet7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EShopMVCNet7.Areas.Admin.Controllers
{
    public class EventController : AdminBaseController
    {
        public EventController(EshopDbContext db) : base(db)
        {
        }

        public IActionResult Index(int page = 1)
        {
            var events = _db.AppEvens
                               .Select(p => new EventsListItemVM
                               {
                                   Id = p.Id,
                                   NameEven = p.NameEven,
                                   CoverImgEven = p.CoverImgEven,
                                   ContentEven = p.ContentEven,
                                   DiscountFrom = p.DiscountFrom,
                                   DiscountTo = p.DiscountTo,
                               })
                               .OrderByDescending(p => p.Id)
                               .ToPagedList(page, PER_PAGE);
            return View(events);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //
        [HttpPost]
        public IActionResult Create(EventsUpdinVM eventsVM, [FromServices] IWebHostEnvironment env)
        {
            //Xác thực dữ liệu
            if (ModelState.IsValid == false)
            {
                return View(eventsVM);
            }
            //sao chép dữ liệu từ ViewModel sang model
            var events = new AppEvens
            {
                NameEven = eventsVM.NameEven,
                ContentEven = eventsVM.ContentEven,
                DiscountFrom = eventsVM.DiscountFrom,
                DiscountTo = eventsVM.DiscountTo,
                CreatedAt = DateTime.Now,
            };
            //hình ảnh(CoverImgEven)
            events.CoverImgEven = UploadFile(eventsVM.CoverImgEven, env.WebRootPath);

            try
            {
                _db.Add(events);
                _db.SaveChanges();
                SetSuccessMesg("Thêm sự kiện thành công");
            }
            catch (Exception ex)
            {
                SetErrorMesg(ex.Message);
            }
            return RedirectToAction(nameof(Create));
        }

        //Update sự kiện
        public IActionResult Update(int id)
        {
            var data = _db.AppEvens
                .Select(p => new EventsUpdinVM
                {
                    Id = p.Id,
                    NameEven = p.NameEven,
                    ContentEven = p.ContentEven,
                    DiscountFrom = p.DiscountFrom,
                    DiscountTo = p.DiscountTo,
                })
                .Where(p => p.Id == id).SingleOrDefault();

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sự kiện");
                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }
        [HttpPost]
        public IActionResult Update(int id, EventsUpdinVM evenVM, [FromServices] IWebHostEnvironment env)
        {
            ModelState.Remove("CoverImgEven");
            //xác thực dữ liệu
            if (ModelState.IsValid == false)
            {
                return View(evenVM);
            }
            //tìm
            var oldEven = _db.AppEvens.Find(id);

            if (oldEven == null)
            {
                return RedirectToAction(nameof(Index));
            }
            //update
            oldEven.NameEven = evenVM.NameEven;
            oldEven.ContentEven = evenVM.ContentEven;
            oldEven.DiscountFrom = evenVM.DiscountFrom;
            oldEven.DiscountTo = evenVM.DiscountTo;

            //hình ảnh bìa(CoverImg)
            if (oldEven.ContentEven is not null)
            {
                //xóa ảnh bìa củ ảnh cover
                System.IO.File.Delete(env.WebRootPath + oldEven.CoverImgEven);
                //Update ảnh bìa mới
                oldEven.CoverImgEven = UploadFile(evenVM.CoverImgEven, env.WebRootPath);
            }
            
            //save
            try
            {
                _db.SaveChanges();
                SetSuccessMesg("Cập nhật thành công!!");
            }
            catch (Exception ex)
            {
                SetErrorMesg("Cập nhật không thành công!!" + ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        //xóa sự kiện
        public IActionResult Delete(int id, [FromServices] IWebHostEnvironment env)
        {
            var data = _db.AppEvens.Find(id);

            if (data == null)
            {
                SetErrorMesg("Không tìm thấy sự kiện");
                return RedirectToAction(nameof(Index));
            }
            try
            {
                //Xóa dữ liệu trong db
                _db.Remove(data);
                //xóa ảnh sản phẩm trong disk
                //ảnh cover
                System.IO.File.Delete(Path.Combine(env.WebRootPath, data.CoverImgEven.TrimStart('/')));

                SetSuccessMesg($"Xóa sản phẩm [{data.NameEven}] thành công!");
            }
            catch (Exception ex)
            {
                SetErrorMesg("Xóa không được" + ex.Message);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

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
