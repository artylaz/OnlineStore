using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.SuperuserViewModel
{
    public class EditPurchaseHistoryVM
    {
        public List<ProductVM> Products { get; set; }
        public List<User> Users { get; set; }
        public List<PurchaseHistory> PurchaseHistories { get; set; }
    }
}
