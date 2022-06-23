namespace OnlineStore.DAL.Entities
{
    public partial class ProductInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
