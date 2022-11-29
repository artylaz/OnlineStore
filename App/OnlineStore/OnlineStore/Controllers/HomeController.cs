using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.StaticModels;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnlineStore_DbContext db;
        public HomeController(OnlineStore_DbContext db)
        {
            this.db = db;

            IndexMenuVM.Categorys = db.Categories.Where(c => c.CategoryId == null).Include(c => c.InverseCategoryNavigation).ToList();

        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.First().Value);
                ViewData["AmountBasket"] = db.Baskets.Where(b => b.UserId == userId).Count();
            }

            var latestProducts = db.Products
                .Skip(Math.Max(0, db.Products.Count() - 10)).Include(p => p.Pictures).ToList();

            return View(new IndexVM { Products = latestProducts });
        }
    }
}
