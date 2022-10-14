using AutoMapper;
using OnlineStore.BLL.DTO;
using OnlineStore.BLL.Interfaces;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BLL.Services
{
    public class ProductService : IProductService
    {
        readonly IUnitOfWork unitOfWork;

        readonly IMapper mapper;

        public ProductService(IUnitOfWork uow, IMapper mapper)
        {
            unitOfWork = uow;
            this.mapper = mapper;
        }

        public IEnumerable<ProductDTO> GetLatestProducts()
        {
            var products = unitOfWork.Products.GetAll();

            if (products.Count() >= 10)
            {
                var latestProducts = products.Skip(Math.Max(0, products.Count() - 10));

                var latestProductsDTO = mapper.Map<IEnumerable<ProductDTO>>(latestProducts);

                return latestProductsDTO;
            }
            else
                return null;

        }

        public IEnumerable<CategoryDTO> GetCategoryBranch(int idCategory)
        {
            var category = unitOfWork.Categories.Get(idCategory);

            var categories = new List<Category> { category };

            while (true)
            {
                if (category.CategoryId != null)
                {
                    category = unitOfWork.Categories.Get((int)category.CategoryId);
                    categories.Add(category);
                }
                else
                    break;
            }

            var categoriesDTO = mapper.Map<List<CategoryDTO>>(categories);
            categoriesDTO.Reverse();

            return categoriesDTO;
        }

        public IEnumerable<ProductDTO> GetProducts(int idCategory)
        {
            var products = unitOfWork.Products.Find(p => p.CategoryId == idCategory);

            var categoriesDTO = mapper.Map<IEnumerable<ProductDTO>>(products);

            return categoriesDTO;
        }

        public IEnumerable<ProductDTO> GetProductsAll()
        {
            var products = unitOfWork.Products.GetAll();

            var categoriesDTO = mapper.Map<IEnumerable<ProductDTO>>(products);

            return categoriesDTO;
        }
    }
}
