using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportAttendanceSystem.Models;

namespace SportAttendanceSystem.ViewModels
{
    /// <summary>
    /// 
    /// StudentsAttendances ViewModel has a list of students and a bool value to mark if they are present or not
    /// this ViewModel makes it easier for the teacher to go through all students at once
    /// rather than open "Add attendance" for each student then go back and repeat
    /// the same process, this is too time consuming and provides a bad user experience.
    ///
    /// Used in Views/Attendance/Add.cshtml
    /// 
    /// </summary>
    public class StudentsAttendances
    {
        public Student Student { get; set; }
        public bool IsPresent { get; set; }

    }

}