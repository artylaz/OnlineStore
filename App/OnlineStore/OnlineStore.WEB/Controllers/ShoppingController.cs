using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BLL.Interfaces;
using OnlineStore.WEB.Models;
using OnlineStore.WEB.Models.ShoppingViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.WEB.Controllers
{
    public class ShoppingController : Controller
    {
        IProductService productService;
        readonly IMapper mapper;
        public ShoppingController(IProductService serv, IMapper mapper)
        {
            productService = serv;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult ShowProducts(CategoryVM categoryVM)
        {
            var productsDTO = productService.GetProducts(categoryVM.Id);
            var products = mapper.Map<IEnumerable<ProductVM>>(productsDTO);

            var categoryBranchDTO = productService.GetCategoryBranch(categoryVM.Id);
            var categoryBranch = mapper.Map<IEnumerable<CategoryVM>>(categoryBranchDTO);

            var shopVM = new ShoppingVM { Products = products, CategoryBranch = categoryBranch.ToArray() };

            return View(shopVM);
        }

        [HttpGet]
        public IActionResult ShowProduct(ProductVM productVM)
        {
            return View(productVM);
        }
    }
}
