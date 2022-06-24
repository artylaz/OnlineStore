namespace OnlineStore.BLL.DTO
{
    public partial class RolesPermissionDTO
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual PermissionDTO Permission { get; set; }
        public virtual RoleDTO Role { get; set; }
    }
}
