using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Asp.netproject.mvc.Models
{
    public class EmployeeReportModel
    {
        public int EmployeeId { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public String DesignationName { get; set; }
        public string GradeName { get; set; }


    }
}