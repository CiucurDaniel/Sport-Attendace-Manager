using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportAttendanceSystem.Models;

namespace SportAttendanceSystem.ViewModels
{
    /// <summary>
    /// 
    /// StudentPromotionViewModel used to display students as well as their attendances and promotion status
    /// as a rule, in order to be promoted you need to have (min) 10 attendances
    ///
    /// Used in Views/Attendance/
    /// </summary>
    public class StudentPromotionViewModel
    {
        public Student Student { get; set; }
        public string SportName { get; set; }
        public int Attendances { get; set; }
        public string IsPromoted { get; set; }
    }
}