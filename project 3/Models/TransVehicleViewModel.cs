using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_3.Models
{
    public class TransVehicleViewModel
    {
        public int الرقم { get; set; }
        public string اسم_السائق { get; set; }
        public string رقم_العربية { get; set; }
        public string الموديل { get; set; }
        public string سريال_العربية { get; set; }
        public string هاتف { get; set; }
        public string العنوان { get; set; }
        public int رقم_الوكيل { get; set; }
        public string اسم_الوكيل { get; set; }
    }
}