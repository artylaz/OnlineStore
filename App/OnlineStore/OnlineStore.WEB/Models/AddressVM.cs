namespace OnlineStore.WEB.Models
{
    public partial class AddressVM
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Housing { get; set; }
        public int? StoreId { get; set; }

        public virtual StoreVM Store { get; set; }
    }
}
