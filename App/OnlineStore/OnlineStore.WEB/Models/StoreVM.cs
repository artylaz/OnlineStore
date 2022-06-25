using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class StoreVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AddressVM> Addresses { get; set; }
        public virtual ICollection<StoresProductVM> StoresProducts { get; set; }
        public virtual ICollection<StoresUserVM> StoresUsers { get; set; }
    }
}
