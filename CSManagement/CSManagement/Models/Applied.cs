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
    
    public partial class Applied
    {
        public int APP_NO { get; set; }
        public Nullable<int> APP_ReNO { get; set; }
        public Nullable<int> APP_GenNO { get; set; }
        public Nullable<int> APP_Status { get; set; }
    
        public virtual Generation Generation { get; set; }
        public virtual Register_SC Register_SC { get; set; }
    }
}
