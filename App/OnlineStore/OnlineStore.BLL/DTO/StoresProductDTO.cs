namespace OnlineStore.BLL.DTO
{
    public partial class StoresProductDTO
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual StoreDTO Store { get; set; }
    }
}
