using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.Data
{
    public partial class MonitorDatabase
    {
        public int IdItem { get; set; }
        public string Hostname { get; set; }
        public string NtUsername { get; set; }
        public string NameOperation { get; set; }
        public string NameTable { get; set; }
        public string NameColumns { get; set; }
        public string IdRecord { get; set; }
        public string OldRecord { get; set; }
        public string NewRecord { get; set; }
        public DateTime? DataModificationRecord { get; set; }
    }
}
