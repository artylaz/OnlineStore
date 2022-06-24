namespace OnlineStore.BLL.DTO
{
    public partial class BasketDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; }

        public virtual ProductDTO Product { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
