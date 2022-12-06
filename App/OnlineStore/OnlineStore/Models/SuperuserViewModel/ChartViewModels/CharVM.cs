using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Models.SuperuserViewModel.ChartViewModels
{
    public class CharVM
    {
        public List<string> Dates { get; set; }
        public List<decimal> Prices { get; set; }
        public List<int> Amounts { get; set; }
    }
}
