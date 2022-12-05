using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Models.AdminViewModels
{
    public class EditCategoryVM
    {
        public List<Category> Categories { get; set; }
        public List<Category> ParentCategories { get; set; }
    }
}
