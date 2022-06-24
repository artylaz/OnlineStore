using System;
using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int? RoleId { get; set; }

        public virtual RoleDTO Role { get; set; }
        public virtual ICollection<BasketDTO> Baskets { get; set; }
        public virtual ICollection<PurchaseHistoryDTO> PurchaseHistories { get; set; }
        public virtual ICollection<StoresUserDTO> StoresUsers { get; set; }
    }
}
