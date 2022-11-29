using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ShowBasketVM
    {
        public List<Basket> Baskets { get; set; }

        public decimal? TotalPrice
        {
            get
            {
                if (Baskets != null)
                {
                    decimal? totalPrice = 0;
                    foreach (var item in Baskets)
                    {
                        if (item.Product != null)
                            totalPrice += (item.Product.Price * item.AmountProduct);
                    }
                    return totalPrice;
                }
                return null;
            }
        }
    }
}
