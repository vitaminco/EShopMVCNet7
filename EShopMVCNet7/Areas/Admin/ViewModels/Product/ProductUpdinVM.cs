using EShopMVCNet7.Models;
using System.ComponentModel.DataAnnotations;

namespace EShopMVCNet7.Areas.Admin.ViewModels.Product
{
    public class ProductUpdinVM
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        [Required]
        public string Slug { get; set; }
        [MaxLength(500)]
        [Required]
        public string Summary { get; set; }
        public string Content { get; set; }
        public IFormFile CoverImg { get; set; }
        [Range(0,long.MaxValue)] //long.max là số tối đa của long
        [Required]
        public int InStock { get; set; }
        [Range(0, long.MaxValue)] //long.max là số tối đa của long
        [Required]
        public double Price { get; set; }
        [Range(0, long.MaxValue)] //long.max là số tối đa của long
        public double? DiscountPrice { get; set; }
        public DateTime? DiscountFrom { get; set; }
        public DateTime? DiscountTo { get; set; }
        [Required]
        public int CategoryId { get; set; }
       
        //1 sp có nhiều hình ảnh
        public IFormFileCollection ProductImages { get; set; }

        public string? CoverImgPath { get; set; }
        public List<string>? ProductImgPath {  get; set; } = new List<string>(); //tránh lỗi 500
    }
}
