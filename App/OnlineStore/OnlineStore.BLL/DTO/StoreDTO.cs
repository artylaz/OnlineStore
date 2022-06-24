using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AddressDTO> Addresses { get; set; }
        public virtual ICollection<StoresProductDTO> StoresProducts { get; set; }
        public virtual ICollection<StoresUserDTO> StoresUsers { get; set; }
    }
}
