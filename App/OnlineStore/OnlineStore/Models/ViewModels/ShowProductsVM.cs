using OnlineStore.Data;
using OnlineStore.Models.SortPhilPag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ShowProductsVM
    {
        public List<Product> Products { get; set; }
        public Category Category { get; set; }
        public List<Characteristic> Characteristics { get; set; }
        public List<Characteristic> CheckedCharacteristics { get; set; }
        public SortState SortOrder { get; set; }
        public PageViewModel PageVM { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public int CoutProduct { get; set; }

        public string GetNamPages(int namPage)
        {
            if (CoutProduct < 9)
                return "Показано " + (1 + (9 * (namPage - 1))) + "-" + (CoutProduct + (9 * (namPage - 1))) + " из " + CoutProduct + " результатов";
            else
                return "Показано " + (1 + (9 * (namPage - 1))) + "-" + (9 + (9 * (namPage - 1))) + " из " + CoutProduct + " результатов";

        }
    }
}
