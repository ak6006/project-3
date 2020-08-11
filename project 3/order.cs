//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace project_3
{
    using System;
    using System.Collections.Generic;
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.order_has_product = new HashSet<order_has_product>();
            this.transvehcile_has_order = new HashSet<transvehcile_has_order>();
            this.barcodes = new HashSet<barcode>();
        }
    
        public int order_id { get; set; }
        public int customers_Customers_id { get; set; }
        public Nullable<int> users_user_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string notic { get; set; }
        public int store_store_id { get; set; }
        public Nullable<int> order_state { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual store store { get; set; }
        public virtual user user { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_has_product> order_has_product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<transvehcile_has_order> transvehcile_has_order { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<barcode> barcodes { get; set; }
    }
}
