namespace OnlineStore.DAL.Entities
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Housing { get; set; }
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
    }
}
