using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeManagementWebApp.Models
{
    [Table("dbo.Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Employee Number is required.")]
        [MaxLength(6, ErrorMessage = "Maximum length for the Employee Number is 6 characters.")]
        [RegularExpression(@"^[A-Za-z0-9]+$")]
        [DisplayName("Employee Number")]
        public string EmpNo { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(15, ErrorMessage = "Maximum length for First Name is 15 characters.")]
        [RegularExpression(@"^[A-Za-z]+$")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name is required.")]
        [StringLength(15, ErrorMessage = "Maximum length for the Last Name is 15 characters.")]
        [RegularExpression(@"^[A-Za-z]+$")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birth Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "Contact Number must start with '09' and have 11 digits.")]
        [Range(09000000000, 09999999999, ErrorMessage = "Contact Number must be a 11-digit number starting with '09'.")]
        [DisplayName("Contact Number")]
        public long ContactNo { get; set; }


        [EmailAddress]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Email Address should have a @ domain")]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
    }
}