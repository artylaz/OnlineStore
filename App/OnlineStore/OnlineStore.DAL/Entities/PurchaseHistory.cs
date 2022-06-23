using System;

namespace OnlineStore.DAL.Entities
{
    public partial class PurchaseHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AmountProduct { get; set; }
        public DateTime DatePurchase { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
