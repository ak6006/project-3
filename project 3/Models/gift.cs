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
    
    public partial class gift
    {
        public int giftid { get; set; }
        public string giftname { get; set; }
        public int giftBagsCount { get; set; }
        public byte[] giftimg { get; set; }
        public string userid { get; set; }
        public System.DateTime SysStartTime { get; set; }
        public System.DateTime SysEndTime { get; set; }
    }
}
