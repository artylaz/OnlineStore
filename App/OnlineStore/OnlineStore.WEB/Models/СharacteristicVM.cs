﻿using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class СharacteristicVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacteristicValueVM> CharacteristicValues { get; set; }
    }
}
