using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.StaticModels;
using OnlineStore.Models.SuperuserViewModel;
using OnlineStore.Models.SuperuserViewModel.ChartViewModels;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class SuperuserController : Controller
    {
        private readonly OnlineStore_DbContext db;
        //private readonly int userId;
        public SuperuserController(OnlineStore_DbContext db)
        {
            this.db = db;
            //userId = int.Parse(User.Claims.First().Value);
        }
        #region PurchaseHistory

        [Authorize(Roles = "superuser")]
        [HttpGet]
        public async Task<IActionResult> EditPurchaseHistory()
        {
            int userId = int.Parse(User.Claims.First().Value);

            List<PurchaseHistory> purchaseHistories = await db.PurchaseHistories.ToListAsync();
            List<User> users = await db.Users.Where(u => u.Id != userId).ToListAsync();
            List<Product> products = await db.Products.Include(p => p.Category).Include(p => p.ProductCharacteristics).ToListAsync();
            List<ProductVM> productVMs = new();

            foreach (Product product in products)
            {
                productVMs.Add(
                    new ProductVM
                    {
                        Id = product.Id,
                        ProductCharacteristics = product.ProductCharacteristics.Select(pc => pc.CharacteristicId).ToList(),
                        Category = product.Category,
                        CategoryId = product.CategoryId,
                        Description = product.Description,
                        Name = product.Name,
                        OnSale = product.OnSale,
                        Price = product.Price,
                        Rating = product.Rating,
                    });
            }

            var editPurchaseHistoryVM = new EditPurchaseHistoryVM
            {
                Products = productVMs,
                PurchaseHistories = purchaseHistories,
                Users = users,
            };
            IndexMenuVM.Categorys = db.Categories.Where(c => c.CategoryId == null && c.OnSale == true).Include(c => c.InverseCategoryNavigation).ToList();
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(editPurchaseHistoryVM);
        }

        [Authorize(Roles = "superuser")]
        [HttpPost]
        public async Task<IActionResult> EditPurchaseHistory(PurchaseHistory purchaseHistory)
        {

            if (purchaseHistory.Id != 0)
            {
                db.Entry(purchaseHistory).State = EntityState.Modified;
            }
            else
            {
                await db.PurchaseHistories.AddAsync(purchaseHistory);
            }
            await db.SaveChangesAsync();
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditPurchaseHistory");
        }
        [Authorize(Roles = "superuser")]
        [HttpGet]
        public async Task<IActionResult> RemovePurchaseHistory(PurchaseHistory purchaseHistory)
        {

            if (purchaseHistory.Id != 0)
            {
                db.Remove(purchaseHistory);
                await db.SaveChangesAsync();
            }
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditPurchaseHistory");
        }
        #endregion

        #region User

        [Authorize(Roles = "superuser")]
        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            int userId = int.Parse(User.Claims.First().Value);
            List<User> users = await db.Users.Where(u => u.Id != userId).ToListAsync();
            List<Role> roles = await db.Roles.ToListAsync();

            var editUserVM = new EditUserVM
            {
                Users = users,
                Roles = roles,
            };
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(editUserVM);
        }

        [Authorize(Roles = "superuser")]
        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {

            if (user.Id != 0)
            {
                db.Entry(user).State = EntityState.Modified;
            }
            else
            {
                await db.Users.AddAsync(user);
            }
            await db.SaveChangesAsync();
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditUser");
        }
        #endregion

        #region Role
        //[Authorize(Roles = "superuser")]
        //[HttpPost]
        //public async Task<IActionResult> EditUser(Role role)
        //{

        //    if (role.Id != 0)
        //    {
        //        db.Entry(role).State = EntityState.Modified;
        //    }
        //    else
        //    {
        //        await db.Roles.AddAsync(role);
        //    }
        //    await db.SaveChangesAsync();

        //    return RedirectToAction("AdminAccount", new AdminVM { Status = "PurchaseHistory" });
        //}
        //[Authorize(Roles = "superuser")]
        //[HttpGet]
        //public async Task<IActionResult> RemovePurchaseHistory(Role role)
        //{

        //    if (role.Id != 0)
        //    {
        //        var users = db.Users.Where(u => u.RoleId == role.Id);
        //        if (users != null)
        //        {
        //            db.Users.RemoveRange(users);
        //            db.Remove(role);
        //            await db.SaveChangesAsync();
        //        }
        //    }


        //    return RedirectToAction("AdminAccount", new AdminVM { Status = "Role" });
        //}
        #endregion

        #region MonitorDatabase
        [Authorize(Roles = "superuser")]
        [HttpGet]
        public async Task<IActionResult> ShowMonitorDatabase()
        {
            List<MonitorDatabase> monitorDatabase = await db.MonitorDatabases.ToListAsync();
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(monitorDatabase);
        }
        #endregion

        #region Analytics
        [Authorize(Roles = "superuser")]
        [HttpGet]
        public async Task<IActionResult> Analytics(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                ViewData["Ex"] = "Начальная дата не может быть больше конечной";
                return View(new AnalyticsVM { ShowIs = false });
            }

            if (startDate == default || endDate == default)
            {
                startDate = DateTime.Now.AddYears(-1);
                endDate = DateTime.Now;
            }

            List<PurchaseHistory> purchaseHistories = await db.PurchaseHistories.
                Where(p => p.DatePurchase >= startDate && p.DatePurchase <= endDate)
                .Include(p => p.User).Include(p => p.Product).OrderBy(p => p.DatePurchase).ToListAsync();


            var dates = purchaseHistories
                .GroupBy(p => new { p.DatePurchase.Month, p.DatePurchase.Year })
                .Select(p => p.FirstOrDefault().DatePurchase)
                .OrderBy(p => p)
                .ToList();

            var prices = new List<decimal>();
            var amountProducts = new List<int>();
            foreach (var date in dates)
            {
                decimal price = 0.00m;
                foreach (var item in purchaseHistories.Where(p => p.DatePurchase.Month == date.Month && p.DatePurchase.Year == date.Year))
                {
                    price += item.Product.Price * item.AmountProduct;
                }
                prices.Add(price);

                int amountProduct = 0;
                var pH = purchaseHistories.Where(p => p.DatePurchase.Month == date.Month && p.DatePurchase.Year == date.Year);
                foreach (var item in pH)
                {
                    amountProduct += item.AmountProduct;
                }
                amountProducts.Add(amountProduct);
            }



            CharVM charVM = new() { Dates = dates.Select(d => d.ToString("Y")).ToList(), Amounts = amountProducts, Prices = prices };

            var analyticsVM = new AnalyticsVM
            {
                EndDate = endDate,
                StartDate = startDate,
                PurchaseHistories = purchaseHistories,
                ShowIs = true,
                CharVM = charVM
            };
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(analyticsVM);
        }
        #endregion

        #region Report
        [Authorize(Roles = "superuser")]
        [HttpGet]
        public async Task<IActionResult> Report(ReportVM reportVM)
        {
            if (reportVM.StartDate > reportVM.EndDate)
            {
                ViewData["Ex"] = "Начальная дата не может быть больше конечной";
                return RedirectToAction("Analytics");
            }

            List<PurchaseHistory> purchaseHistories = await db.PurchaseHistories.
                Where(p => p.DatePurchase >= reportVM.StartDate && p.DatePurchase <= reportVM.EndDate)
                .Include(p => p.User).Include(p => p.Product).OrderBy(p => p.DatePurchase).ToListAsync();

            reportVM.PurchaseHistories = purchaseHistories;
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(reportVM);
        }
        #endregion
    }
}
