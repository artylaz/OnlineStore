namespace OnlineStore.BLL.DTO
{
    public partial class PictureDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? ProductId { get; set; }

        public virtual ProductDTO Product { get; set; }
    }
}
