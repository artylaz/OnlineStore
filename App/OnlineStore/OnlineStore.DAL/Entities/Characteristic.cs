using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.DAL.Entities
{
    public partial class Characteristic
    {
        public Characteristic()
        {
            CharacteristicValues = new HashSet<CharacteristicValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacteristicValue> CharacteristicValues { get; set; }
    }
}
