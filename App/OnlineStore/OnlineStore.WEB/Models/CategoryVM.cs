using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class CategoryVM
    {
        public CategoryVM()
        {
            InverseCategoryNavigation = new HashSet<CategoryVM>();
            Products = new HashSet<ProductVM>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public virtual CategoryVM CategoryNavigation { get; set; }
        public virtual ICollection<CategoryVM> InverseCategoryNavigation { get; set; }
        public virtual ICollection<ProductVM> Products { get; set; }
    }
}
