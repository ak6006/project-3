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
    
    public partial class shift_has_workers
    {
        public int shift_shift_id { get; set; }
        public int workers_worker_id { get; set; }
        public string userid { get; set; }
        public System.DateTime SysStartTime { get; set; }
        public System.DateTime SysEndTime { get; set; }
    
        public virtual shift shift { get; set; }
        public virtual worker worker { get; set; }
    }
}
