namespace EShopMVCNet7.ViewModels.Cart
{
    public class CartListItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverImg { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public DateTime? DiscountFrom { get; set; }
        public DateTime? DiscountTo { get; set; }
        //số lượng sp trong giỏ
        public int QuantityInCart { get; set; }

        //logic xác định khuyến mãi hay không
        public bool IsDiscount
        {
            get
            {
                var isDiscount = false;
                if (DiscountPrice.HasValue)
                {
                    var startDate = DiscountFrom ?? DateTime.MinValue;
                    var endDate = DiscountTo ?? DateTime.MaxValue;
                    isDiscount = startDate <= DateTime.Now && endDate >= DateTime.Now;
                }
                return isDiscount;
            }
        }
        //giá cuối cùng
        public double FinalPrice
        {
            get
            {
                return IsDiscount ? DiscountPrice.Value : Price;
            }
        }
    }
}
