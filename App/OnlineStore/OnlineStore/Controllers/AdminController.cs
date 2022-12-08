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
        //private readonly int userId;
        public AdminController(OnlineStore_DbContext db)
        {
            this.db = db;
            //userId = int.Parse(User.Claims.First().Value);
        }

        #region AdminAccount
        [Authorize(Roles = "admin,superuser")]
        public async Task<IActionResult> AdminAccount(AdminVM adminVM)
        {
            int userId = int.Parse(User.Claims.First().Value);
            User user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var myAccountVM = new AdminVM
            {
                User = new RegisterViewModel
                {
                    Email = user.Email,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    FirstName = user.FirstName,
                    Password = user.Password
                },
            };

            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(myAccountVM);
        }
        #endregion

        #region AdminShops
        [Authorize(Roles = "admin,superuser")]
        public async Task<IActionResult> AdminShops(AdminVM adminVM)
        {
            int userId = int.Parse(User.Claims.First().Value);
            List<PurchaseHistory> userPurchaseHistories = await db.PurchaseHistories
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .ToListAsync();

            var myAccountVM = new AdminVM
            {
                UserPurchaseHistories = userPurchaseHistories
            };

            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(myAccountVM);
        }
        #endregion

        #region Product

        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> EditProduct()
        {
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
            List<Characteristic> characteristics = await db.Characteristics.ToListAsync();
            List<Category> productCategories = await db.Categories.Where(c => c.CategoryId != null && c.OnSale != false).ToListAsync();

            var editProductVM = new EditProductVM
            {
                Products = productVMs,
                ProductCategories = productCategories,
                Characteristics = characteristics,
            };
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(editProductVM);
        }

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

            if (product.ProductCharacteristics != null)
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

            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditProduct");
        }
        #endregion

        #region Category

        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> EditCategory()
        {
            List<Category> categories = await db.Categories.ToListAsync();
            List<Category> parentCategories = await db.Categories.Where(c => c.CategoryId == null).ToListAsync();

            var editCategoryVM = new EditCategoryVM
            {
                ParentCategories = parentCategories,
                Categories = categories
            };

            return View(editCategoryVM);
        }

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
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditCategory");
        }
        #endregion

        #region Characteristic

        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> EditCharacteristic()
        {
            List<Characteristic> characteristics = await db.Characteristics.ToListAsync();

            var editCharacteristicVM = new EditCharacteristicVM
            {
                Characteristics = characteristics,
            };
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(editCharacteristicVM);
        }

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
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditCharacteristic");
        }
        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> RemoveCharacteristic(Characteristic characteristic)
        {

            if (characteristic.Id != 0)
            {
                var chPrs = db.ProductCharacteristics.Where(pc => pc.CharacteristicId == characteristic.Id);
                db.RemoveRange(chPrs);
                db.Remove(characteristic);
                await db.SaveChangesAsync();
            }
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditCharacteristic");
        }
        #endregion

        #region Picture

        [Authorize(Roles = "admin,superuser")]
        [HttpGet]
        public async Task<IActionResult> EditPicture()
        {
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
            List<Picture> pictures = await db.Pictures.ToListAsync();

            var editPictureVM = new EditPictureVM
            {
                Products = productVMs,
                Pictures = pictures,
            };
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return View(editPictureVM);
        }

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
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditPicture");
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
            int userId = int.Parse(User.Claims.First().Value);
            ViewData["AmountBasket"] = await db.Baskets.Where(b => b.UserId == userId).CountAsync();
            return RedirectToAction("EditPicture");
        }
        #endregion

    }
}
