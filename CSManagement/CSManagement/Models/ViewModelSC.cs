using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSManagement.Models
{
    public class ViewModelSC
    {
        public string SC_NameTH { get; set; }
        public string Gen_Name { get; set; }
        public string Gen_Date { get; set; }
        public string REG_Name { get; set; }
        public string REG_Tel { get; set; }
        public string REG_Email { get; set; }
        public int Gen_NO { get; set; }
        public string APP_ReNO { get; set; }
        public Nullable<int> APP_GenNO { get; set; }
        public Nullable<int> APP_Status { get; set; }
        public Nullable<System.DateTime> APP_Date { get; set; }
    }
}