using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.AdminViewModels;
using OnlineStore.Models.StaticModels;
using OnlineStore.Models.SuperuserViewModel;
using OnlineStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class SuperuserController : Controller
    {
        private readonly OnlineStore_DbContext db;

        public SuperuserController(OnlineStore_DbContext db)
        {
            this.db = db;
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
            return View(monitorDatabase);
        }
        #endregion

    }
}
