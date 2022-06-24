using System;

namespace OnlineStore.WEB.Models
{
    public partial class PurchaseHistoryVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AmountProduct { get; set; }
        public DateTime DatePurchase { get; set; }

        public virtual ProductVM Product { get; set; }
        public virtual UserVM User { get; set; }
    }
}
