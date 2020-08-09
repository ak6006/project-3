using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project_3.Models
{
    public class CustomerViewModel
    {
        [Key]
        public int رقم_الوكيل { get; set; }
        public int معرف { get; set; }
        public string الاسم { get; set; }
        public string دولة { get; set; }
        public string المحافظة { get; set; }
        public string المدينة { get; set; }
        public string بريد_الكتروني { get; set; }
        public string عنوان { get; set; }
        public string تلفون { get; set; }
        public string هاتف { get; set; }
        public string فاكس { get; set; }

    }
}