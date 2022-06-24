namespace OnlineStore.WEB.Models
{
    public partial class RolesPermissionVM
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public virtual PermissionVM Permission { get; set; }
        public virtual RoleVM Role { get; set; }
    }
}
