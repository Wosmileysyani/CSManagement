using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSManagement.Models
{
    public partial class V_Student
    {
        [Display(Name = "รหัสนักศึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "กรุณาใส่ให้ครบ 11 หลัก")]
        public string Stu_ID { get; set; }
        [Display(Name = "คำนำหน้าชื่อ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_Title { get; set; }
        [Display(Name = "ชื่อ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_Name { get; set; }
        [Display(Name = "นามสกุล")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_Surname { get; set; }
        [Display(Name = "เพศ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<bool> Stu_Sex { get; set; }
        [Display(Name = "วัน/เดือน/ปีเกิด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<System.DateTime> Stu_Birthday { get; set; }
        [Display(Name = "อีเมลล์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_Email { get; set; }
        [Display(Name = "เบอร์โทร")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_Tel { get; set; }
        [Display(Name = "ที่อยู่")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_Address { get; set; }
        [Display(Name = "โรงเรียนเดิม")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Stu_School { get; set; }
        [Display(Name = "วุฒิการศึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Stu_OldEdu { get; set; }
        [Display(Name = "รูปภาพ")]
        public string Stu_Img { get; set; }
    }
    [MetadataType(typeof(V_Student))]
    public partial class Student { }
}