using EShopMVCNet7.Models;

namespace EShopMVCNet7.ViewModels.Home
{
    public class ProductListItemVM
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }                // Làm đẹp url

        public string Summary { get; set; }             // Mô tả ngắn

        public string CoverImg { get; set; }

        public double Price { get; set; }

        public double? DiscountPrice { get; set; }

        public DateTime? DiscountFrom { get; set; }

        public DateTime? DiscountTo { get; set; }

        public string CategoryName { get; set; }
    }
}
