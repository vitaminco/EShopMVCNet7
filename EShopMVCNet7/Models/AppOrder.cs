using EShopMVCNet7.Common;

namespace EShopMVCNet7.Models
{
    public class AppOrder
    {
        public AppOrder() {
            Detail = new HashSet<AppOrderDetal>();
        }
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public int? CustomerId { get; set; }
        public OrderStatus? Status { get; set; } // chờ tiếp nhận, đã nhận, đã giao, đã hủy
        public DateTime? CreatedAt { get; set; }

        virtual public ICollection<AppOrderDetal> Detail { get; set; }
    }
}
