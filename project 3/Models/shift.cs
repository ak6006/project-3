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
    
    public partial class shift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public shift()
        {
            this.store_has_product = new HashSet<store_has_product>();
            this.workers = new HashSet<worker>();
        }
    
        public int shift_id { get; set; }
        public string shiftName { get; set; }
        public int shiftAdmin_shiftAdmin_id { get; set; }
    
        public virtual shiftadmin shiftadmin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<store_has_product> store_has_product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<worker> workers { get; set; }
    }
}