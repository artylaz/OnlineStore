using OnlineStore.Data;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.AdminViewModels
{
    public class EditPictureVM
    {
        public List<Picture> Pictures { get; set; }
        public List<ProductVM> Products { get; set; }
    }
}
