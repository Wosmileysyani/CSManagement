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
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.Departments = new HashSet<Department>();
        }
    
        public int Course_ID { get; set; }
        public string Course_NameTH { get; set; }
        public string Course_NameENG { get; set; }
        public Nullable<int> Course_Year { get; set; }
        public string Course_DegreeTH { get; set; }
        public string Course_DegreeENG { get; set; }
        public Nullable<int> Course_Credit { get; set; }
        public string Couese_PDF { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Departments { get; set; }
    }
}
