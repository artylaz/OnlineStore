﻿using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class BrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductVM> Products { get; set; }
    }
}
