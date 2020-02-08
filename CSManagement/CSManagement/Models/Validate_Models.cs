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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "วันเกิด")]
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

        [Display(Name = "สถานะการศึกษา")]
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DIS_DateOUT { get; set; }

        [Display(Name = "วันที่จำหน่าย")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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
        public string HIS_Year { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime Pj_Date { get; set; }

        [Display(Name = "ประเภท")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        public string Pj_TypePj { get; set; }

        [Display(Name = "ไฟล์")]
        public string Pj_File { get; set; }

        [Display(Name = "Github")]
        public string Pj_Github { get; set; }

        [Display(Name = "รายละเอียด")]
        [Required(ErrorMessage = "กรุณาใส่ข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string Pj_Description { get; set; }

        [Display(Name = "ลิงค์เว็บไซต์")]
        public string Pj_Linkweb { get; set; }

        [Display(Name = "วิดีโอ")]
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
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

    public partial class VGeneration
    {
        [Display(Name = "รหัสรุ่น")]
        public int Gen_NO { get; set; }

        [Display(Name = "ชื่อหลักสูตรอบรม")]
        public Nullable<int> Gen_SCID { get; set; }

        [Display(Name = "รุ่นที่")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Gen_Name { get; set; }

        [Display(Name = "จำนวนที่เหลือ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public Nullable<int> Gen_Member { get; set; }

        [Display(Name = "จำนวนที่รับ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public Nullable<int> Gen_MemberMax { get; set; }

        [Display(Name = "รายละเอียด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string Gen_Details { get; set; }

        [Display(Name = "ปีที่อบรม")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Gen_Year { get; set; }

        [Display(Name = "วันที่จัดอบรม")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Gen_Date { get; set; }

        [Display(Name = "สถานที่จัดอบรม")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Gen_Location { get; set; }

        [Display(Name = "ราคา")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public Nullable<double> Gen_Fee { get; set; }

        [Display(Name = "ข้อความตอบกลับอีเมล")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string Gen_TextForMail { get; set; }

        [Display(Name = "สถานะการรับสมัคร")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public Nullable<int> Gen_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Applied> Applieds { get; set; }
        public virtual Short_Course Short_Course { get; set; }
        public virtual Gen_Status Gen_Status1 { get; set; }
    }
    [MetadataType(typeof(VGeneration))]
    public partial class Generation { }

    public partial class VGen_Status
    {
        [Display(Name = "รหัสสถานะ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int Gen_Status1 { get; set; }

        [Display(Name = "สถานะการรับสมัคร")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Gen_Name { get; set; }
    }
    [MetadataType(typeof(VGen_Status))]
    public partial class Gen_Status { }

    public partial class V_Short_Course
    {
        [Display(Name = "รหัส")]
        public int SC_ID { get; set; }

        [Display(Name = "ชื่อหลักสูตรอบรม")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string SC_NameTH { get; set; }
    }
    [MetadataType(typeof(V_Short_Course))]
    public partial class Short_Course { }

    public partial class VRegister_SC
    {
        [Display(Name = "รหัสบัตรประชาชน")]
        [Required(ErrorMessage = "กรุณากรอกหมายเลข")]
        public string REG_IDCard { get; set; }

        [Display(Name = "ชื่อ-นามสกุล")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string REG_Name { get; set; }

        [Display(Name = "ที่อยู่")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string REG_Address { get; set; }

        [Display(Name = "เบอร์โทร")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.PhoneNumber)]
        public string REG_Tel { get; set; }

        [Display(Name = "อีเมล")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.EmailAddress)]
        public string REG_Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Applied> Applieds { get; set; }
    }
    [MetadataType(typeof(VRegister_SC))]
    public partial class Register_SC { }

    public partial class VSyllabu
    {
        [Display(Name = "รหัส")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int Sy_ID { get; set; }

        [Display(Name = "เรื่อง")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Sy_Name { get; set; }

        [Display(Name = "รายละเอียด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string Sy_Details { get; set; }

        [Display(Name = "รูปภาพ")]
        public string Sy_Img { get; set; }
    }
    [MetadataType(typeof(VSyllabu))]
    public partial class Syllabu { }

    public partial class VNews
    {
        [Display(Name = "รหัส")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public int New_ID { get; set; }

        [Display(Name = "หัวข้อข่าวสาร")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string New_Name { get; set; }

        [Display(Name = "รายละเอียด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DataType(DataType.MultilineText)]
        public string New_Details { get; set; }

        [Display(Name = "รูปภาพ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string New_Img { get; set; }

        [Display(Name = "วันที่")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> New_Date { get; set; }
    }
    [MetadataType(typeof(VNews))]
    public partial class News { }
}