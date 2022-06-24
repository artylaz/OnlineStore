using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolesPermissionDTO> RolesPermissions { get; set; }
        public virtual ICollection<UserDTO> Users { get; set; }
    }
}
