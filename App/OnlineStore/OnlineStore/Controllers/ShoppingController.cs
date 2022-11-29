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
                products = db.Products.Include(p => p.Pictures).ToList();
            else
                products = db.Products.Where(p => p.CategoryId == category.Id).Include(p => p.Pictures).ToList();

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
                Pictures = db.Pictures.Where(p => p.ProductId == product.Id).ToList(),
                Category = product.Category,
                Price = product.Price,
                Rating = product.Rating
            };

            return View(productVM);
        }

        [HttpGet]
        public IActionResult AddToBasket(AddToBasketVM addToBasketVM)
        {
            var userId = int.Parse(User.Claims.First().Value);

            var category = db.Categories.FirstOrDefault(c => c.Id == addToBasketVM.CategoryId);

            var basket = db.Baskets.FirstOrDefault(c => c.UserId == userId);

            if (basket == null)
            {
                basket = new Basket { UserId = userId, ProductId = addToBasketVM.ProductId, AmountProduct = addToBasketVM.AmountProduct };
                db.Baskets.Add(basket);
            }
            else
            {
                var basketProduct = db.Baskets.Find(addToBasketVM.ProductId, userId);

                if (basketProduct != null)
                {
                    basketProduct.AmountProduct += addToBasketVM.AmountProduct;
                    db.Entry(basketProduct).State = EntityState.Modified;
                }
                else
                {
                    basketProduct = new Basket { AmountProduct = addToBasketVM.AmountProduct, ProductId = addToBasketVM.ProductId, UserId = userId };
                    db.Baskets.Add(basketProduct);
                }

            }

            db.SaveChanges();

            return RedirectToAction("ShowProducts", category);
        }

        [HttpGet]
        public IActionResult ShowBasket()
        {
            List<Basket> baskets = new();

            if (int.TryParse(User.Claims.First().Value, out int userId))
            {
                baskets = db.Baskets.Where(b => b.UserId == userId).Include(b => b.User).Include(b => b.Product).ThenInclude(p => p.Pictures).ToList();
            }

            return View(new ShowBasketVM { Baskets = baskets });
        }

        [HttpGet]
        public IActionResult RemoveBasket(Basket basket)
        {
            if (basket != null)
            {
                db.Baskets.Remove(basket);
                db.SaveChanges();
            }

            return RedirectToAction("ShowBasket");
        }

        [HttpGet]
        public IActionResult BuyProducts(List<Basket> Baskets)
        {
            if (Baskets.Count > 0)
            {
                foreach (var item in Baskets)
                {
                    db.PurchaseHistories.Add(new PurchaseHistory
                    {
                        AmountProduct = (int)item.AmountProduct,
                        ProductId = item.ProductId,
                        UserId = item.UserId,
                        DatePurchase = DateTime.Now
                    });
                }
                db.Baskets.RemoveRange(Baskets);

                db.SaveChanges();
            }
            return RedirectToAction("ShowBasket");
        }
    }
}
