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
    using System.Collections.Generic;
    
    public partial class order_has_product_History
    {
        public int order_has_product_Id { get; set; }
        public int order_order_id { get; set; }
        public int product_product_id { get; set; }
        public int measurement_measure_id { get; set; }
        public int weight_weight_id { get; set; }
        public Nullable<double> Quantity { get; set; }
        public Nullable<int> order_has_product_Pages_Count { get; set; }
        public Nullable<int> order_has_product_dept_count { get; set; }
        public Nullable<int> order_has_product_state { get; set; }
        public Nullable<System.DateTime> order_has_product_in_date { get; set; }
        public Nullable<System.DateTime> order_has_product_out_date { get; set; }
        public string userid { get; set; }
        public System.DateTime SysStartTime { get; set; }
        public System.DateTime SysEndTime { get; set; }
    }
}