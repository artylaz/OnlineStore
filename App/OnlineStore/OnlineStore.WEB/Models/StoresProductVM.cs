namespace OnlineStore.WEB.Models
{
    public partial class StoresProductVM
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; }

        public virtual ProductVM Product { get; set; }
        public virtual StoreVM Store { get; set; }
    }
}
