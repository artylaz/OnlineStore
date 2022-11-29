using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class AddToBasketVM
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public int? AmountProduct { get; set; } = 1;
    }
}
