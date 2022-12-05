using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.SuperuserViewModel
{
    public class EditUserVM
    {
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
    }
}
