﻿using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class CharacteristicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacteristicValueDTO> CharacteristicValues { get; set; }
    }
}
