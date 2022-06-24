namespace OnlineStore.WEB.Models
{
    public class StoresUserVM
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }

        public virtual StoreVM Store { get; set; }
        public virtual UserVM User { get; set; }
    }
}
