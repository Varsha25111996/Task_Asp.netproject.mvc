using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Asp.netproject.mvc.Models
{
    public class DesignationGradeModel
    {
        public int DesignationGradeId { get; set; }
        public string GradeName { get; set; }
        public int EmployeeDesignationId { get; set; }
        public Boolean IsActive { get; set; }

    }
}