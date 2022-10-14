using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class CharacteristicVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacteristicValueVM> CharacteristicValues { get; set; }
    }
}
