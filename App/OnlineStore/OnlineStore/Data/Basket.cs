using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.Data
{
    public partial class Basket
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
