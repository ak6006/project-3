using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_3.Models
{
    public class OrderVehicleViewModel
    {
        public int OId { get; set; }
        public int PId { get; set; }
        public int WId { get; set; }
        public string CName { get; set; }
        public string DName { get; set; }
        public int All { get; set; }
        public int Remaining { get; set; }
        public int Counter { get; set; }
        public string BarCode { get; set; }
    }
}