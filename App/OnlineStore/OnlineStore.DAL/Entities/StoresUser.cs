namespace OnlineStore.DAL.Entities
{
    public class StoresUser
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }

        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
    }
}
