using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.Data
{
    public partial class Characteristic
    {
        public Characteristic()
        {
            ProductCharacteristics = new HashSet<ProductCharacteristic>();
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
    }
}
