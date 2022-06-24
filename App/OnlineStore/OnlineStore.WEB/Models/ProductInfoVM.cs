namespace OnlineStore.WEB.Models
{
    public partial class ProductInfoVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ProductId { get; set; }

        public virtual ProductVM Product { get; set; }
    }
}
