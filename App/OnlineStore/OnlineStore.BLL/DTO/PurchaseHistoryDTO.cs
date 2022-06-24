using System;

namespace OnlineStore.BLL.DTO
{
    public partial class PurchaseHistoryDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AmountProduct { get; set; }
        public DateTime DatePurchase { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
