namespace OnlineStore.WEB.Models
{
    public partial class PictureVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? ProductId { get; set; }

        public virtual ProductVM Product { get; set; }
    }
}
