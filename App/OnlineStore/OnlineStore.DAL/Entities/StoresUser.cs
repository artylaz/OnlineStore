using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.DAL.Entities
{
    public partial class StoresUser
    {
        public int StoreId { get; set; }
        public int UserId { get; set; }

        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
    }
}
