using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_3.Models
{ 
    public class MeasurementViewModel
    {
        public int رقم_الوحدة { get; set; }
        public string اسم_وحدة_القياس { get; set; }
        public int measurement_Count_per_unit { get; set; }


    }
}