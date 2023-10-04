namespace EShopMVCNet7.Models
{
    public class AppProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int ProductId { get; set; }

        // 1Hình thuộc 1 sp nào đó
        public AppProduct Product { get; set; }
    }
}
