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

        [Display(Name ="สถานะการศึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Stu_StatusID { get; set; }
    }
    [MetadataType(typeof(V_Student))]
    public partial class Student { }

    public partial class V_Status
    {
        [Display(Name = "สถานะ")]
        public int Status_ID { get; set; }

        [Display(Name = "สถานะการศึกษา")]
        public string Status_Name { get; set; }
    }
    [MetadataType(typeof(V_Status))]
    public partial class Status { }

    public partial class V_CESup
    {
        [Display(Name = "รหัสรายละเอียด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int CESUB_ATNO { get; set; }

        [Display(Name = "ใบเบิกที่")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CESUB_NO { get; set; }

        [Display(Name = "ชื่อรายการ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CESUB_Name { get; set; }

        [Display(Name = "สถานะ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> CESUB_Status { get; set; }

        [Display(Name = "หมายเลขครุภัณฑ์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> CE_ATNO { get; set; }
    }
    [MetadataType(typeof(V_CESup))]
    public partial class CESup { }

    public partial class V_ComputerEquipment
    {
        [Display(Name = "หมายเลข")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int CE_ATNO { get; set; }

        [Display(Name = "ใบเบิกที่")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CE_NO { get; set; }

        [Display(Name = "วันที่รับเข้า")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<System.DateTime> CE_DateIN { get; set; }

        [Display(Name = "ชื่อรายการ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CE_Name { get; set; }

        [Display(Name = "หมายเลขครุภัณฑ์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CE_Noce { get; set; }

        [Display(Name = "จำนวนชิ้น")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> CE_Piece { get; set; }

        [Display(Name = "ราคา/หน่วย")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<double> CE_Price { get; set; }

        [Display(Name = "ราคารวม")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<double> CE_PriceTotal { get; set; }

        [Display(Name = "ผู้เบิก")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CE_TeaID { get; set; }

        [Display(Name = "งบประมาณ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string CE_Budget { get; set; }

        [Display(Name = "สถานะ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> CE_Status { get; set; }


        public virtual Teacher Teacher { get; set; }
    }
    [MetadataType(typeof(V_ComputerEquipment))]
    public partial class ComputerEquipment { }

    public partial class V_Course
    {
        [Display(Name = "รหัสหลักสูตร")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int Course_ID { get; set; }

        [Display(Name = "ชื่อหลักสูตรภาษาไทย")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Course_NameTH { get; set; }

        [Display(Name = "ชื่อหลักสูตรภาษาอังกฤษ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Course_NameENG { get; set; }

        [Display(Name = "หลักสูตรประจำปี")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Course_Year { get; set; }

        [Display(Name = "ชื่อปริญญาภาษาไทย")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Course_DegreeTH { get; set; }

        [Display(Name = "ชื่อปริญญาภาษาอังกฤษ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Course_DegreeENG { get; set; }

        [Display(Name = "จำนวนหน่วยกิตทั้งหมด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Course_Credit { get; set; }
    }
    [MetadataType(typeof(V_Course))]
    public partial class Course { }

    public partial class V_Department
    {
        [Display(Name = "รหัสหมวดหมู่วิชา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int Dep_ID { get; set; }

        [Display(Name = "หมวดหมู่วิชา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Dep_Name { get; set; }

        [Display(Name = "จำนวนหน่วยกิต")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Dep_Credit { get; set; }

        [Display(Name = "รหัสหลักสูตร")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Dep_CourseID { get; set; }

        public virtual Course Course { get; set; }
    }
    [MetadataType(typeof(V_Department))]
    public partial class Department { }

    public partial class V_Dispose
    {
        [Display(Name = "รหัสจำหน่าย")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int DIS_ATNO { get; set; }

        [Display(Name = "รหัสครุภัณฑ์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> CE_ATNO { get; set; }

        [Display(Name = "วันที่ทำรายการ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<System.DateTime> DIS_DateOUT { get; set; }

        [Display(Name = "วันที่จำหน่าย")]
        public Nullable<System.DateTime> DIS_DateAPP { get; set; }

        [Display(Name = "สถานะ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> DIS_Status { get; set; }
    }
    [MetadataType(typeof(V_Dispose))]
    public partial class Dispose { }

    public partial class V_History
    {
        [Display(Name = "รหัส")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int HIS_ID { get; set; }

        [Display(Name = "รหัสนักศึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string HIS_StuID { get; set; }

        [Display(Name = "ประกอบอาชีพ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> HIS_Job { get; set; }

        [Display(Name = "ตำแหน่งงาน")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string HIS_Jobname { get; set; }

        [Display(Name = "ปีที่เข้าทำงาน")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<System.DateTime> HIS_Year { get; set; }

        [Display(Name = "ชื่อบริษัท")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string HIS_Company { get; set; }

        public virtual Job Job { get; set; }

        public virtual Student Student { get; set; }
    }
    [MetadataType(typeof(V_History))]
    public partial class History { }

    public partial class V_Job
    {
        [Display(Name = "รหัส")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int JOB_ID { get; set; }

        [Display(Name = "ประกอบอาชีพ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string JOB_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<History> Histories { get; set; }
    }
    [MetadataType(typeof(V_Job))]
    public partial class Job { }

    public partial class V_Project
    {
        [Display(Name = "รหัสโปรเจ็ค")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int Pj_ID { get; set; }

        [Display(Name = "รหัสนักศึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_StuID { get; set; }

        [Display(Name = "อาจารย์ที่ปรึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_TeaID { get; set; }

        [Display(Name = "ชื่อภาษาไทย")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_NameTH { get; set; }

        [Display(Name = "ชื่อภาษาอังกฤษ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_NameENG { get; set; }

        [Display(Name = "วันที่")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public System.DateTime Pj_Date { get; set; }

        [Display(Name = "ประเภท")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_TypePj { get; set; }

        [Display(Name = "ประเภทไฟล์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int Pj_Typefile { get; set; }

        [Display(Name = "ไฟล์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_File { get; set; }

        [Display(Name = "Github")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_Github { get; set; }

        [Display(Name = "รายละเอียด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_Description { get; set; }

        [Display(Name = "ลิงค์เว็บไซต์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_Linkweb { get; set; }

        [Display(Name = "วิดีโอ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_Video { get; set; }

        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
    [MetadataType(typeof(V_Project))]
    public partial class Project { }

    public partial class V_School
    {
        [Display(Name = "รหัสโรงเรียน")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int SCH_ID { get; set; }

        [Display(Name = "ชื่อโรงเรียน")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string SCH_Name { get; set; }

        [Display(Name = "จังหวัด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string SCH_Province { get; set; }

        [Display(Name = "เบอร์โทรศัพท์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string SCH_Tel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
    [MetadataType(typeof(V_School))]
    public partial class School { }

    public partial class V_Teacher
    {
        [Display(Name = "รหัสอาจารย์")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Tea_ID { get; set; }

        [Display(Name = "คำนำหน้า")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<int> Tea_TitleID { get; set; }

        [Display(Name = "ชื่อ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Tea_Name { get; set; }

        [Display(Name = "นามสกุล")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Tea_Surname { get; set; }

        [Display(Name = "วัน/เดือน/ปีเกิด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public Nullable<System.DateTime> Tea_Birth { get; set; }

        [Display(Name = "รูปภาพประจำตัว")]
        public string Tea_Img { get; set; }

        [Display(Name = "ความชํานาญการ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string Tea_Export { get; set; }

        [Display(Name = "วุฒิการศึกษา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string Tea_LvEdu { get; set; }

        [Display(Name = "ประจำสาขา")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Tea_Program { get; set; }

        [Display(Name = "ตำแหน่งวิชาการ")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Tea_Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComputerEquipment> ComputerEquipments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }
        public virtual Title Title { get; set; }
    }
    [MetadataType(typeof(V_Teacher))]
    public partial class Teacher { }

    public partial class V_Title
    {
        [Display(Name = "รหัส")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public int Title_ID { get; set; }

        [Display(Name = "คำนำหน้า")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Title_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
    [MetadataType(typeof(V_Title))]
    public partial class Title { }
}