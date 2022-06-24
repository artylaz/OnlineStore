﻿using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class RoleVM
    {
        public RoleVM()
        {
            RolesPermissions = new HashSet<RolesPermissionVM>();
            Users = new HashSet<UserVM>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolesPermissionVM> RolesPermissions { get; set; }
        public virtual ICollection<UserVM> Users { get; set; }
    }
}
