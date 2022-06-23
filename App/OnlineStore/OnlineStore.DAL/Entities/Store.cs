using System.Collections.Generic;

namespace OnlineStore.DAL.Entities
{
    public partial class Store
    {
        public Store()
        {
            Addresses = new HashSet<Address>();
            StoresProducts = new HashSet<StoresProduct>();
            StoresUsers = new HashSet<StoresUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<StoresProduct> StoresProducts { get; set; }
        public virtual ICollection<StoresUser> StoresUsers { get; set; }
    }
}
