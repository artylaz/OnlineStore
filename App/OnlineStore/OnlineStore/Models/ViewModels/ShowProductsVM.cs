using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ShowProductsVM
    {
        public List<Product> Products { get; set; }
        public Category Category { get; set; }
    }
}
