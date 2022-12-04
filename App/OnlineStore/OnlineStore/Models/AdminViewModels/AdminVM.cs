using OnlineStore.Data;
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
        public string Status { get; set; }
        public List<PurchaseHistory> UserPurchaseHistories { get; set; }
        public RegisterViewModel User { get; set; }

        public decimal GetPrice(int amountProduct, decimal price)
        {
            return price * amountProduct;
        }

        public List<ProductVM> Products { get; set; }
        public List<Category> ProductCategories { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> ParentCategories { get; set; }
        public List<Characteristic> Characteristics { get; set; }
        public List<PurchaseHistory> PurchaseHistories { get; set; }
        public List<Picture> Pictures { get; set; }
        public List<User> Users { get; set; }
        public List<MonitorDatabase> MonitorDatabases { get; set; }
        public List<Role> Roles { get; set; }

    }
}
