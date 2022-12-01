using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.UserViewModels
{
    public class MyAccountVM
    {
        public List<PurchaseHistory> PurchaseHistories { get; set; }
        public RegisterViewModel User { get; set; }

        public decimal GetPrice(int amountProduct, decimal price)
        {
            return price * amountProduct;
        }
    }
}
