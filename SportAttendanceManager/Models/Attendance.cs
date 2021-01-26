using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SportAttendanceSystem.Models
{
    public class Attendance
    {
        [Key, Column(Order = 1)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdAttendance { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ReportDate { get; set; }

        // add the student who has this attendance
        public int IdStudent { get; set; }
        public Student Student { get; set; }
    }
}