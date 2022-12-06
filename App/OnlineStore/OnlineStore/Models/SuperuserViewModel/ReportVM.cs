using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.SuperuserViewModel
{
    public class ReportVM
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PurchaseHistory> PurchaseHistories { get; set; }
    }
}
