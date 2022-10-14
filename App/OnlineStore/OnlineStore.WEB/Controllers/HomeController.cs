using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BLL.Interfaces;
using OnlineStore.WEB.Models;
using OnlineStore.WEB.Models.StaticModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.WEB.Controllers
{
    public class HomeController : Controller
    {
        ICategoryService categoryService;
        IProductService productService;
        readonly IMapper mapper;
        public HomeController(ICategoryService categoryService, IProductService productService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.productService = productService;
            this.mapper = mapper;


            IndexMenuVM.categoryVMs = mapper.Map<IEnumerable<CategoryVM>>(categoryService.GetCategoriesMenu()).ToList();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var latestProductsDTO = productService.GetLatestProducts();

            if(latestProductsDTO != null)
            {
                return View(mapper.Map<IEnumerable<ProductVM>>(latestProductsDTO));
            }
            else
                return View();

        }
    }
}
