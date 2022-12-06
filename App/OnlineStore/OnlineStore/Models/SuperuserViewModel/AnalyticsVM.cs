using OnlineStore.Data;
using OnlineStore.Models.SuperuserViewModel.ChartViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.SuperuserViewModel
{
    public class AnalyticsVM
    {
        public bool ShowIs { get; set; } = false;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PurchaseHistory> PurchaseHistories { get; set; }
        public CharVM CharVM { get; set; }
    }
}
