//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dispose
    {
        public int DIS_ID { get; set; }
        public string DIS_NO { get; set; }
        public Nullable<System.DateTime> DIS_DateOUT { get; set; }
        public Nullable<System.DateTime> DIS_DateAPP { get; set; }
        public Nullable<int> DIS_Status { get; set; }
    
        public virtual ComputerEquipment ComputerEquipment { get; set; }
    }
}
