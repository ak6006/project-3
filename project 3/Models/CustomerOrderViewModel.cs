using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project_3.Models
{
    public class CustomerOrderViewModel
    {
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public string Notes { get; set; }
        public int OrderId { get; set; }
        public int OrderHasProductId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{MM-dd-yyyy:0}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WieghtId { get; set; }
        public int? WieghtName { get; set; }
        public int MeasureId { get; set; }
        public string MeasureName { get; set; }
        public string quantity { get; set; }
        public int CarId { get; set; }
      
    }
    public class OrderCars
    {
        public int VId { get; set; }
        public string DriverName { get; set; }
    }
}