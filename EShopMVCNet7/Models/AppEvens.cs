namespace EShopMVCNet7.Models
{
    public class AppEvens
    {
        public int Id { get; set; }
        public string NameEven { get; set; }
        public string ContentEven { get; set; }
        public string CoverImgEven { get; set; }
        public DateTime? DiscountFrom { get; set; }
        public DateTime? DiscountTo { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
