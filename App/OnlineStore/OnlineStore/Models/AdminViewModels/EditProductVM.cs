using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace OnlineStore.Models.AdminViewModels
{
    public class EditProductVM
    {
        public List<ProductVM> Products { get; set; }
        public List<Category> ProductCategories { get; set; }
        public List<Characteristic> Characteristics { get; set; }
        public decimal GetPrice(int amountProduct, decimal price)
        {
            return price * amountProduct;
        }
    }
}
