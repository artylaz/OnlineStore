using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }

        public virtual CategoryDTO CategoryNavigation { get; set; }
        public virtual ICollection<CategoryDTO> InverseCategoryNavigation { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }
    }
}
