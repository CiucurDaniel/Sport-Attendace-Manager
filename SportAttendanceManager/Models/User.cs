using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportAttendanceSystem.Models
{
    public class User
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Minimum length for name is 3")]
        [StringLength(50, MinimumLength = 3)]

        public string FirstName { get; set; }


        [Required(ErrorMessage = "Minimum length for name is 3")]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "This is not a valid email address")]
        public string Email { get; set; }


        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$", ErrorMessage = "Password should contain at least 8 characters, one uppercase, one lowercase and one number")]
        public string Password { get; set; }


        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }


        //add one to many Sport that the teacher has
        public List<Sport> Sports { get; set; }


        public string FullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}