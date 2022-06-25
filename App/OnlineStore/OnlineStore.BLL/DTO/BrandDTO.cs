﻿using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class BrandDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
