using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.AdminViewModels;
using OnlineStore.Models.UserViewModels;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly OnlineStore_DbContext db;

        public AdminController(OnlineStore_DbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> AdminAccount()
        {
            int userId = int.Parse(User.Claims.First().Value);

            User user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            List<PurchaseHistory> purchaseHistories = await db.PurchaseHistories
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .ToListAsync();

            List<Product> products = await db.Products.Include(p => p.Category).ToListAsync();
            List<Category> productCategories = await db.Categories.Where(c => c.CategoryId != null).ToListAsync();
            List<Characteristic> characteristics = await db.Characteristics.ToListAsync();

            var myAccountVM = new AdminVM
            {
                PurchaseHistories = purchaseHistories,
                User = new RegisterViewModel
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    FirstName = user.FirstName,
                    Password = user.Password
                },
                Products = products,
                ProductCategories = productCategories,
                Characteristics = characteristics,
            };


            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(myAccountVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (product.Id != 0)
            {
                db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                await db.Products.AddAsync(product);
            }
            await db.SaveChangesAsync();

            return RedirectToAction("AdminAccount");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveProduct(Product product)
        {
            if (product != null && product.Id != 0)
            {
                db.Remove(product);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("AdminAccount");
        }

    }
}
