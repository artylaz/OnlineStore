using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.Data;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.Models.SortPhilPag;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ShowProducts(Category category, List<Characteristic> characteristics, string priceFiltr = "1000 P - 3000 P", SortState sortOrder = SortState.NameAsc, int page = 1)
        {
            string[] stringArr = new string[6];
            if (priceFiltr != null)
                stringArr = priceFiltr.Split(new char[] { ' ' });

            category = await db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.First().Value);
                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            }

            List<Product> products = new();

            if (category == null || category.CategoryId == null && category.Id == 0)
                products = await db.Products.Include(p => p.Pictures).Include(p => p.ProductCharacteristics).ThenInclude(p => p.Characteristic).ToListAsync();
            else if (category.CategoryId == null)
            {
                var categories = await db.Categories.Where(c => c.CategoryId == category.Id).ToListAsync();

                foreach (var item in categories)
                {
                    var categoriesList = await db.Products.Where(p => p.CategoryId == item.Id).Include(p => p.Pictures).Include(p => p.ProductCharacteristics).ThenInclude(p => p.Characteristic).ToListAsync();
                    products.AddRange(categoriesList);
                }
            }
            else
                products = await db.Products.Where(p => p.CategoryId == category.Id).Include(p => p.Pictures).Include(p => p.ProductCharacteristics).ThenInclude(p => p.Characteristic).ToListAsync();

            var showProductsVM = new ShowProductsVM();

            //Фильтрация
            var productsF = new List<Product>();
            if (characteristics == null || characteristics.Count() == 0)
                productsF = products;
            else
                foreach (var item in characteristics)
                {
                    productsF.AddRange(products.Where(p => p.ProductCharacteristics.Any(ph => ph.CharacteristicId == item.Id) == true));
                }

            if (stringArr[0] != null && stringArr[3] != null)
                productsF = productsF.Where(p => p.Price >= int.Parse(stringArr[0]) && p.Price <= int.Parse(stringArr[3])).ToList();

            //Сортировка
            productsF = sortOrder switch
            {
                SortState.NameAsc => productsF.OrderBy(s => s.Name).ToList(),
                SortState.NameDesc => productsF.OrderByDescending(s => s.Name).ToList(),
                SortState.PriceAsc => productsF.OrderBy(s => s.Price).ToList(),
                SortState.PriceDesc => productsF.OrderByDescending(s => s.Price).ToList(),
                _ => productsF.OrderBy(s => s.Name).ToList(),
            };

            showProductsVM.CoutProduct = productsF.Count;

            //Пагинация
            int pageSize = 9;
            var count = productsF.Count;
            productsF = productsF.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            showProductsVM.Products = productsF;
            showProductsVM.Category = category;
            showProductsVM.CheckedCharacteristics = characteristics;
            showProductsVM.Characteristics = db.Characteristics.ToList();
            showProductsVM.SortOrder = sortOrder;
            showProductsVM.PageVM = new PageViewModel(count, page, pageSize);
            showProductsVM.PriceFiltr = stringArr[0] + " P - " + stringArr[3] + " P";


            return View(showProductsVM);
        }

        [HttpGet]
        public async Task<IActionResult> ShowProduct(Product product)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(User.Claims.First().Value);
                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            }

            var characteristics = await db.ProductCharacteristics.Where(p => p.ProductId == product.Id)
                .Include(p => p.Characteristic).Select(p => p.Characteristic).ToListAsync();

            var productVM = new ShowProductVM
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Characteristics = characteristics,
                Pictures = await db.Pictures.Where(p => p.ProductId == product.Id).ToListAsync(),
                Category = product.Category,
                Price = product.Price,
                Rating = product.Rating
            };

            return View(productVM);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToBasket(AddToBasketVM addToBasketVM)
        {

            var userId = int.Parse(User.Claims.First().Value);

            var category = await db.Categories.FirstOrDefaultAsync(c => c.Id == addToBasketVM.CategoryId);

            var basket = await db.Baskets.FirstOrDefaultAsync(c => c.UserId == userId);


            if (basket == null)
            {
                basket = new Basket { UserId = userId, ProductId = addToBasketVM.ProductId, AmountProduct = addToBasketVM.AmountProduct };
                await db.Baskets.AddAsync(basket);
            }
            else
            {
                var basketProduct = await db.Baskets.FirstOrDefaultAsync(b => b.UserId == userId && b.ProductId == addToBasketVM.ProductId);

                if (basketProduct != null)
                {
                    basketProduct.AmountProduct += addToBasketVM.AmountProduct;
                    db.Entry(basketProduct).State = EntityState.Modified;
                }
                else
                {
                    basketProduct = new Basket { AmountProduct = addToBasketVM.AmountProduct, ProductId = addToBasketVM.ProductId, UserId = userId };
                    await db.Baskets.AddAsync(basketProduct);
                }

            }

            await db.SaveChangesAsync();

            if (User.Identity.IsAuthenticated)
            {
                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            }

            return RedirectToAction("ShowProducts", category);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ShowBasket()
        {
            List<Basket> baskets = new();

            if (int.TryParse(User.Claims.First().Value, out int userId))
            {
                baskets = await db.Baskets.Where(b => b.UserId == userId).Include(b => b.User).Include(b => b.Product).ThenInclude(p => p.Pictures).ToListAsync();

                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            }

            return View(new ShowBasketVM { Baskets = baskets });
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveBasket(Basket basket)
        {
            if (basket != null)
            {
                db.Baskets.Remove(basket);
                await db.SaveChangesAsync();

                var userId = int.Parse(User.Claims.First().Value);
                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            }

            return RedirectToAction("ShowBasket");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> BuyProducts(List<Basket> Baskets)
        {
            if (Baskets.Count > 0)
            {
                foreach (var item in Baskets)
                {
                    await db.PurchaseHistories.AddAsync(new PurchaseHistory
                    {
                        AmountProduct = (int)item.AmountProduct,
                        ProductId = item.ProductId,
                        UserId = item.UserId,
                        DatePurchase = DateTime.Now
                    });
                }
                db.Baskets.RemoveRange(Baskets);

                await db.SaveChangesAsync();

                var userId = int.Parse(User.Claims.First().Value);
                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            }
            return RedirectToAction("ShowBasket");
        }
    }
}
