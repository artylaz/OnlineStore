namespace OnlineStore.WEB.Models
{
    public partial class BasketVM
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; }

        public virtual ProductVM Product { get; set; }
        public virtual UserVM User { get; set; }
    }
}
