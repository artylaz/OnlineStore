using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.DAL.Entities
{
    public partial class ProductCharacteristicValue
    {
        public int ProductId { get; set; }
        public int CharacteristicValueId { get; set; }

        public virtual CharacteristicValue CharacteristicValue { get; set; }
        public virtual Product Product { get; set; }
    }
}
