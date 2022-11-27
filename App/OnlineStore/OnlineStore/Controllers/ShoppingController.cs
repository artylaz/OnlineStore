using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Data;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly OnlineStore_DbContext db;
        public ShoppingController(OnlineStore_DbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult ShowProducts(Category category)
        {
            List<Product> products = new List<Product>();

            if (category.CategoryId == null)
            {
                var categories = db.Categories.Where(c => c.CategoryId == category.Id).ToList();

                foreach (var item in categories)
                {
                    products.AddRange(db.Products.Where(p => p.CategoryId == item.Id).Include(p => p.Pictures));
                }
            }
            else
            {
                products = db.Products.Where(p => p.CategoryId == category.Id).Include(p => p.Pictures).ToList();
            }

            var showProductsVM = new ShowProductsVM { Products = products, Category = category };

            return View(showProductsVM);
        }

        [HttpGet]
        public IActionResult ShowProduct(Product product)
        {
            var characteristics = db.ProductCharacteristics.Where(p => p.ProductId == product.Id)
                .Include(p => p.Characteristic).Select(p => p.Characteristic).ToList();

            var productVM = new ShowProductVM
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Characteristics = characteristics,
                Pictures = db.Pictures.Where(p=>p.ProductId == product.Id).ToList(),
                Category = product.Category,
                Price = product.Price,
                Rating = product.Rating
            };

            return View(productVM);
        }
    }
}
