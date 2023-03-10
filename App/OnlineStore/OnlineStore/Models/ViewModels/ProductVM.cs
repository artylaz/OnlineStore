using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public int? CategoryId { get; set; }
        public bool? OnSale { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public List<int> ProductCharacteristics { get; set; }
    }
}
