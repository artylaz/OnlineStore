using System;
using System.Collections.Generic;

namespace OnlineStore.DAL.Entities
{
    public partial class User
    {
        public User()
        {
            Baskets = new HashSet<Basket>();
            PurchaseHistories = new HashSet<PurchaseHistory>();
            StoresUsers = new HashSet<StoresUser>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
        public virtual ICollection<StoresUser> StoresUsers { get; set; }
    }
}
