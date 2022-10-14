using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.WEB.Models.ShoppingViewModels
{
    public class ShoppingVM
    {
        public IEnumerable<ProductVM> Products { get; set; }
        public CategoryVM[] CategoryBranch { get; set; }
    }
}
