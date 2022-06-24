using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class PermissionVM
    {
        public PermissionVM()
        {
            RolesPermissions = new HashSet<RolesPermissionVM>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolesPermissionVM> RolesPermissions { get; set; }
    }
}
