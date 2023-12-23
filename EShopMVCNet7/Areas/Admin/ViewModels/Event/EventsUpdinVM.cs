using EShopMVCNet7.Models;
using System.ComponentModel.DataAnnotations;

namespace EShopMVCNet7.Areas.Admin.ViewModels.Event
{
    public class EventsUpdinVM
    {
        public int Id { get; set; }
        [Required]
        public string NameEven { get; set; }
        [Required]
        public string ContentEven { get; set; }
        public IFormFile CoverImgEven { get; set; }
        public DateTime? DiscountFrom { get; set; }
        public DateTime? DiscountTo { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
