using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.BLL.Interfaces;
using OnlineStore.BLL.Services;
using OnlineStore.WEB.Models;
using OnlineStore.WEB.Models.StaticModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.WEB.Controllers
{
    public class HomeController : Controller
    {
        ICategoryService categoryService;
        readonly IMapper mapper;
        public HomeController(ICategoryService serv, IMapper mapper)
        {
            categoryService = serv;
            this.mapper = mapper;

            IndexMenuVM.categoryVMs = mapper.Map<IEnumerable<CategoryVM>>(categoryService.GetCategoriesMenu()).ToList();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
