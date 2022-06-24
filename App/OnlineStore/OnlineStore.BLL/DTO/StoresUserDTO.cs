namespace OnlineStore.BLL.DTO
{
    public class StoresUserDTO
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }

        public virtual StoreDTO Store { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
