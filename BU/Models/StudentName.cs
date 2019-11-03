using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSManagement.Models
{
    public partial class StudentName
    {
        public string Stu_ID { get; set; }
        public string Stu_Title { get; set; }
        public string Stu_Name { get; set; }
        public string Stu_Surname { get; set; }
        public Nullable<bool> Stu_Sex { get; set; }
        public Nullable<System.DateTime> Stu_Birthday { get; set; }
        public string Stu_Email { get; set; }
        public string Stu_Tel { get; set; }
        public string Stu_Address { get; set; }
        public Nullable<int> Stu_School { get; set; }
        public string Stu_OldEdu { get; set; }
        public string Stu_Img { get; set; }
        public Nullable<int> Stu_StatusID { get; set; }
    }
}