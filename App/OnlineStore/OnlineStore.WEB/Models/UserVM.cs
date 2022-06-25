using System;
using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class UserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? RoleId { get; set; }

        public virtual RoleVM Role { get; set; }
        public virtual ICollection<BasketVM> Baskets { get; set; }
        public virtual ICollection<PurchaseHistoryVM> PurchaseHistories { get; set; }
        public virtual ICollection<StoresUserVM> StoresUsers { get; set; }
    }
}
