using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class PermissionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolesPermissionDTO> RolesPermissions { get; set; }
    }
}
