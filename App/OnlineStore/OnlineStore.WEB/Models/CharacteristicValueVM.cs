using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class CharacteristicValueVM
    {
        public int Id { get; set; }
        public int СharacteristicId { get; set; }
        public string Value { get; set; }

        public virtual СharacteristicVM Сharacteristic { get; set; }
        public virtual ICollection<ProductCharacteristicValueVM> ProductCharacteristicValues { get; set; }
    }
}
