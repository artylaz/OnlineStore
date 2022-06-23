using System.Collections.Generic;

namespace OnlineStore.DAL.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            RolesPermissions = new HashSet<RolesPermission>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolesPermission> RolesPermissions { get; set; }
    }
}
