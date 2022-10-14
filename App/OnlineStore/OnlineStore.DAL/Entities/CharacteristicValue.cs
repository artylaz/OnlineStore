using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.DAL.Entities
{
    public partial class CharacteristicValue
    {
        public CharacteristicValue()
        {
            ProductCharacteristicValues = new HashSet<ProductCharacteristicValue>();
        }

        public int Id { get; set; }
        public int CharacteristicId { get; set; }
        public string Value { get; set; }

        public virtual Characteristic Characteristic { get; set; }
        public virtual ICollection<ProductCharacteristicValue> ProductCharacteristicValues { get; set; }
    }
}
