using System.ComponentModel.DataAnnotations;

namespace EShopMVCNet7.Areas.Admin.ViewModels.Category
{
    public class CategoryUpdinVM
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Slug { get; set; }//làm đẹp url 
    }
}
