//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project_3.Models
{
    using System;
    
    public partial class SP_flutter_Customer_Orders_Query_Result
    {
        public int order_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string transVehcile_driver_name { get; set; }
        public string transVehcile_num { get; set; }
        public string productName { get; set; }
        public string Quantity { get; set; }
        public string measre_name { get; set; }
        public Nullable<int> order_has_product_state { get; set; }
        public Nullable<System.DateTime> order_has_product_in_date { get; set; }
        public Nullable<System.DateTime> order_has_product_out_date { get; set; }
    }
}