﻿using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.AdminViewModels
{
    public class AdminVM
    {
        public List<PurchaseHistory> PurchaseHistories { get; set; }
        public RegisterViewModel User { get; set; }

        public decimal GetPrice(int amountProduct, decimal price)
        {
            return price * amountProduct;
        }

        public List<Product> Products { get; set; }
        public List<Category> ProductCategories { get; set; }
        public List<Category> Categories { get; set; }
        public List<Characteristic> Characteristics { get; set; }
    }
}
