using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.UserViewModels;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class UserController : Controller
    {
        private readonly OnlineStore_DbContext db;

        public UserController(OnlineStore_DbContext db)
        {
            this.db = db;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyAccount()
        {
            int userId = int.Parse(User.Claims.First().Value);

            User user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            List<PurchaseHistory> purchaseHistories = await db.PurchaseHistories
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .ToListAsync();

            var myAccountVM = new MyAccountVM
            {
                PurchaseHistories = purchaseHistories,
                User = new RegisterViewModel
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    FirstName = user.FirstName,
                    Password = user.Password
                }
            };


            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(myAccountVM);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditAccount(RegisterViewModel model)
        {
            int userId = int.Parse(User.Claims.First().Value);
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
                user.Email = model.Email; 
                user.LastName = model.LastName; 
                user.Phone = model.Phone; 
                user.FirstName = model.FirstName; 
                user.Password = model.Password;

                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();

                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();

                return RedirectToAction("MyAccount");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");

                ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();

                return View();
            }
        }
    }
}
