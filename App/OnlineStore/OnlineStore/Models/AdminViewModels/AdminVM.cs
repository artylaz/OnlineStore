using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.AdminViewModels
{
    public class AdminVM
    {
        public List<PurchaseHistory> UserPurchaseHistories { get; set; }
        public RegisterViewModel User { get; set; }

        public decimal GetPrice(int amountProduct, decimal price)
        {
            return price * amountProduct;
        }

    }
}
