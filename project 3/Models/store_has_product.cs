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
    
    public partial class store_has_product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public store_has_product()
        {
            this.orders = new HashSet<order>();
        }
    
        public int store_store_id { get; set; }
        public int product_product_id { get; set; }
        public int shift_shift_id { get; set; }
        public string barcode_serialNumber { get; set; }
        public int weight_weight_id { get; set; }
        public Nullable<System.DateTime> store_has_productDate { get; set; }
        public Nullable<int> store_has_product_state { get; set; }
    
        public virtual barcode barcode { get; set; }
        public virtual shift shift { get; set; }
        public virtual store store { get; set; }
        public virtual weight weight { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }
        public virtual product product { get; set; }
    }
}
