using EShopMVCNet7.Models;

namespace EShopMVCNet7.ViewModels.Home
{
    public class EventsListItemVM
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
