using OnlineStore.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetProducts(int idCategory);
        IEnumerable<ProductDTO> GetLatestProducts();
        IEnumerable<CategoryDTO> GetCategoryBranch(int idCategory);
        IEnumerable<ProductDTO> GetProductsAll();
    }
}
