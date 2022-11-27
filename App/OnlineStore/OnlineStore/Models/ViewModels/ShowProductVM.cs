using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.ViewModels
{
    public class ShowProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public int? CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Characteristic> Characteristics { get; set; }

    }
}
