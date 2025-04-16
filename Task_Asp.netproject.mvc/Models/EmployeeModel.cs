using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Task_Asp.netproject.mvc.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Please Enter The First Name")]
        [MinLength(2, ErrorMessage = "Minimum 2 Characters Required")]
        [MaxLength(10,ErrorMessage = "Maximum 10 Characters Required")]

        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please Enter The Last Name")]
        [MinLength(2, ErrorMessage = "Minimum 2 Characters Required")]
        [MaxLength(10, ErrorMessage = "Maximum 10 Characters Required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please Enter The Email Address")]
        [EmailAddress(ErrorMessage = "Invalid EmailAddress")]

        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please Enter a Mobile Number")]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage = "Invalid Mobile Number. It must be 10 digits and start with 6,7,8, or 9.")]
        public string PhoneNumber { get; set; }
        public int EmployeeDesignationId { get; set; }
        public int DesignationGradeId { get; set; }
    }
}