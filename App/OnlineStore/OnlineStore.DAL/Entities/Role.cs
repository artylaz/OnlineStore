using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.DAL.Entities
{
    public partial class Role
    {
        public Role()
        {
            RolesPermissions = new HashSet<RolesPermission>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolesPermission> RolesPermissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
