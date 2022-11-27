using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.Data
{
    public partial class ProductCharacteristic
    {
        public int ProductId { get; set; }
        public int CharacteristicId { get; set; }

        public virtual Characteristic Characteristic { get; set; }
        public virtual Product Product { get; set; }
    }
}
