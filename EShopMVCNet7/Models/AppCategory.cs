namespace EShopMVCNet7.Models
{
    public class AppCategory
    {
        public AppCategory()
        {
            Products = new HashSet<AppProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }//làm đẹp url 

        //1DM chưa nhiều SP
        public ICollection<AppProduct> Products { get; set; }
    }
}
