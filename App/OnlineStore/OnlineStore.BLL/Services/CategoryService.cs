using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class CategoryService : ICategoryService
    {
        readonly IUnitOfWork unitOfWork;

        readonly IMapper mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            unitOfWork = uow;
            this.mapper = mapper;
        }

        public IEnumerable<CategoryDTO> GetCategoriesMenu()
        {
            var categories = unitOfWork.Categories.Find(c => c.CategoryId == null);

            var categoriesDTO = mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return categoriesDTO;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }


}
