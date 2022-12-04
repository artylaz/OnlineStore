using Microsoft.AspNetCore.Authorization;
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

        #region AdminAccount
        [Authorize(Roles = "admin,superuser")]
        public async Task<IActionResult> AdminAccount(AdminVM adminVM)
        {
            int userId = int.Parse(User.Claims.First().Value);

            User user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            List<PurchaseHistory> userPurchaseHistories = await db.PurchaseHistories
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .ToListAsync();

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


            List<Category> productCategories = await db.Categories.Where(c => c.CategoryId != null && c.OnSale != false).ToListAsync();

            List<Category> categories = await db.Categories.ToListAsync();
            List<Category> parentCategories = await db.Categories.Where(c => c.CategoryId == null).ToListAsync();

            List<Characteristic> characteristics = await db.Characteristics.ToListAsync();
            List<Picture> pictures = await db.Pictures.ToListAsync();
            List<PurchaseHistory> purchaseHistories = await db.PurchaseHistories.ToListAsync();
            List<User> users = await db.Users.Where(u => u.Id != userId).ToListAsync();
            List<MonitorDatabase> monitorDatabases = await db.MonitorDatabases.ToListAsync();
            List<Role> roles = await db.Roles.ToListAsync();

            var myAccountVM = new AdminVM
            {
                UserPurchaseHistories = userPurchaseHistories,
                User = new RegisterViewModel
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    FirstName = user.FirstName,
                    Password = user.Password
                },
                Products = productVMs,
                ProductCategories = productCategories,
                Characteristics = characteristics,
                Status = adminVM.Status,
                ParentCategories = parentCategories,
                Categories = categories,
                Pictures = pictures,
                PurchaseHistories = purchaseHistories,
                Users = users,
                MonitorDatabases = monitorDatabases,
                Roles = roles,
            };
            ViewData["liShow"] = adminVM.Status;

            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(myAccountVM);
        }
        #endregion

        #region Product
        [Authorize(Roles = "admin,superuser")]
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductVM product)
        {
            var pr = await db.Products.Include(p => p.ProductCharacteristics).FirstOrDefaultAsync(p => p.Id == product.Id);

            if (pr == null)
                pr = new Product();
            else
                pr.ProductCharacteristics.Clear();

            pr.Id = product.Id;
            pr.Category = product.Category;
            pr.CategoryId = product.CategoryId;
            pr.Description = product.Description;
            pr.Name = product.Name;
            pr.OnSale = product.OnSale;
            pr.Price = product.Price;
            pr.Rating = product.Rating;

            foreach (var item in product.ProductCharacteristics)
            {
                pr.ProductCharacteristics.Add(new ProductCharacteristic { CharacteristicId = item, ProductId = product.Id });
            }


            if (pr.Id != 0)
            {
                db.Entry(pr).State = EntityState.Modified;
            }
            else
            {
                await db.Products.AddAsync(pr);
            }
            await db.SaveChangesAsync();


            return RedirectToAction("AdminAccount", new AdminVM { Status = "Product" });
        }
        #endregion

        #region Category
        [Authorize(Roles = "admin,superuser")]
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (category.CategoryId == 0)
                category.CategoryId = null;

            if (category.Id != 0)
            {
                db.Entry(category).State = EntityState.Modified;
            }
            else
            {
                await db.Categories.AddAsync(category);
            }
            await db.SaveChangesAsync();

            return RedirectToAction("AdminAccount", new AdminVM { Status = "Category" });
        }
        #endregion

        #region Characteristic
        [Authorize(Roles = "admin,superuser")]
        [HttpPost]
        public async Task<IActionResult> EditCharacteristic(Characteristic characteristic)
        {

            if (characteristic.Id != 0)
            {
                db.Entry(characteristic).State = EntityState.Modified;
            }
            else
            {
                await db.Characteristics.AddAsync(characteristic);
            }
            await db.SaveChangesAsync();

            return RedirectToAction("AdminAccount", new AdminVM { Status = "Characteristic" });
        }
        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> RemoveCharacteristic(Characteristic characteristic)
        {

            if (characteristic.Id != 0)
            {
                db.Remove(characteristic);
                await db.SaveChangesAsync();
            }


            return RedirectToAction("AdminAccount", new AdminVM { Status = "Characteristic" });
        }
        #endregion

        #region Picture
        [Authorize(Roles = "admin,superuser")]
        [HttpPost]
        public async Task<IActionResult> EditPicture(Picture picture)
        {

            if (picture.Id != 0)
            {
                db.Entry(picture).State = EntityState.Modified;
            }
            else
            {
                await db.Pictures.AddAsync(picture);
            }
            await db.SaveChangesAsync();

            return RedirectToAction("AdminAccount", new AdminVM { Status = "Picture" });
        }
        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> RemovePicture(Picture picture)
        {

            if (picture.Id != 0)
            {
                db.Remove(picture);
                await db.SaveChangesAsync();
            }


            return RedirectToAction("AdminAccount", new AdminVM { Status = "Picture" });
        }
        #endregion

        #region superuser

        #region PurchaseHistory
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

            return RedirectToAction("AdminAccount", new AdminVM { Status = "PurchaseHistory" });
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


            return RedirectToAction("AdminAccount", new AdminVM { Status = "PurchaseHistory" });
        }
        #endregion

        #region User
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

            return RedirectToAction("AdminAccount", new AdminVM { Status = "User" });
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


        #endregion
    }
}
