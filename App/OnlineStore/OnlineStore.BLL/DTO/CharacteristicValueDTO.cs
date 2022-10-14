using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class CharacteristicValueDTO
    {
        public int Id { get; set; }
        public int СharacteristicId { get; set; }
        public string Value { get; set; }

        public virtual CharacteristicDTO Сharacteristic { get; set; }
        public virtual ICollection<ProductCharacteristicValueDTO> ProductCharacteristicValues { get; set; }
    }
}
