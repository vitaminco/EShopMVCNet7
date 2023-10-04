namespace EShopMVCNet7.Models
{
    public class AppProduct
    {
        public AppProduct()
        {
            ProductImages = new HashSet<AppProductImage>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string CoverImg { get; set; }
        public int InStock { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public DateTime? DiscountFrom { get; set; }
        public DateTime? DiscountTo { get; set; }
        public int? View { get; set; }
        public int? CategoryId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        // 1SP có 1DM
        public AppCategory Category { get; set; }
        //1 sp có nhiều hình ảnh
        public ICollection<AppProductImage> ProductImages { get; set; }
    }
}
