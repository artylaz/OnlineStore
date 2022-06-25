using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.DAL.Entities
{
    public partial class StoresProduct
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
