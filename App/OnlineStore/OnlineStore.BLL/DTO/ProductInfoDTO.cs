namespace OnlineStore.BLL.DTO
{
    public partial class ProductInfoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ProductId { get; set; }

        public virtual ProductDTO Product { get; set; }
    }
}
