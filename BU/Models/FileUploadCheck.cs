using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSManagement.Models
{
    public class FileUploadCheck
    {
        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }
        public string UploadUserFile(HttpPostedFileBase file)
        {
            try
            {
                var supportedTypes = new[] { "pdf" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    ErrorMessage = "สามารถอัพโหลดได้เฉพาะ .pdf เท่านั้น";
                    return ErrorMessage;
                }
                else if (file.ContentLength > (filesize * 1024))
                {
                    ErrorMessage = "ขนาดสูงสุดของไฟล์ต้องไม่เกิน" + filesize + "KB";
                    return ErrorMessage;
                }
                else
                {
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "กรุณาอัพโหลดไฟล์ด้วย";
                return ErrorMessage;
            }
        }
    }
}