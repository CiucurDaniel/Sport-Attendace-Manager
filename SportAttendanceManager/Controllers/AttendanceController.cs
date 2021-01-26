using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportAttendanceSystem.DataAccessLayer;
using SportAttendanceSystem.Models;
using SportAttendanceSystem.ViewModels;
using Rotativa;

namespace SportAttendanceSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private SportAttendanceManagerContext db = new SportAttendanceManagerContext();

        // GET: Attendance
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.Student);
            return View(attendances.ToList());
        }

        // GET: Attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            ViewBag.IdStudent = new SelectList(db.Students, "IdStudent", "FirstName");
            return View();
        }

        // POST: Attendance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAttendance,ReportDate,IdStudent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdStudent = new SelectList(db.Students, "IdStudent", "FirstName", attendance.IdStudent);
            return View(attendance);
        }


        public ActionResult AddGroupPresence(IEnumerable<Attendance> attendances)
        {
            // iterate trough each student

            // db.Attendances.Add(attendance);
            //db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Attendance/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdStudent = new SelectList(db.Students, "IdStudent", "FirstName", attendance.IdStudent);
            return View(attendance);
        }

        // POST: Attendance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAttendance,ReportDate,IdStudent")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdStudent = new SelectList(db.Students, "IdStudent", "FirstName", attendance.IdStudent);
            return View(attendance);
        }

        // GET: Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // TODO: Try to add a presence to multiple students at once

        // GET Attendance/Add
        public ActionResult Add(int sportId)
        {

            List<StudentsAttendances> presenceList = new List<StudentsAttendances>();

            IEnumerable<Student> students = from student in db.Students
                                            where student.IdSport == sportId
                                            select student;

            foreach (Student student in students)
            {
                presenceList.Add(new StudentsAttendances { Student = student, IsPresent = false });
            }



            return View(presenceList);
        }

        // POST Attendance/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(List<StudentsAttendances> presentStudents)
        {

            // TODO: Implement

            if (presentStudents == null)
            {
                return Content("You did it, i received data to post but data received is still NULL");
            }
            else
            {
                string data = "";
                foreach (var student in presentStudents)
                {
                    if (student.Student == null)
                    {
                        data += "NULL OBJECT";
                    }
                    else
                    {
                        data += "\n" + student.Student.Email + " is present:" + student.IsPresent + "\n";
                    }

                }
                return Content("You did it, i received data to post: " + presentStudents.Count() + "\n" + data);

            }

        }

        // GET Attendance/PromotionStatus
        public ActionResult PromotionStatus(int teacherId)
        {

            // The ViewModel we need to pass to the view
            List<StudentPromotionViewModel> studentPromotionViewModels = new List<StudentPromotionViewModel>();

            // the ViewBag data we need to pass to the view as well 
            List<string> sports = new List<string>();


            // get sports taught by teacher

            var queryToGetUserSports = from sport in db.Sports
                where sport.IdUser == teacherId
                select sport.IdSport;

            var sportList = queryToGetUserSports.ToList();

            // get all the students of the teacher

            var queryToGetStudentList = from student in db.Students
                where queryToGetUserSports.Contains(student.IdSport)
                select student;

            List<Student> studentList = queryToGetStudentList.ToList();

            for (int index = 0; index < studentList.Count; index++)
            {
                int tempSportId = studentList[index].IdSport;

                var tempSportName = from sport in db.Sports
                    where sport.IdSport == tempSportId
                    select sport.SportName;

                // also add the sport to the ViewBag sport list, if it's not yet there

                List<string> tmp = tempSportName.ToList();
                string currentSport = tmp[0];

                if (!sports.Contains(currentSport))
                {
                    sports.Add(currentSport);
                }


                // compute attendance 
                int attendance = 10;

                studentPromotionViewModels.Add(
                          new StudentPromotionViewModel
                                    {
                                        Student = studentList[index],
                                        SportName = currentSport,
                                        Attendances = attendance,
                                        IsPromoted = attendance >= 10 ? "Promoted" : "Failed",
                                    }
                                    );
            }

            // List
            // ViewModel
            // Student sportName Attendances promoted

            ViewBag.SportNames = sports;

            return View(studentPromotionViewModels);
        }


        // GET Attendance/PrintPdfTemplate
        public ActionResult PrintPdfTemplate(int teacherId)
        {

            // The ViewModel we need to pass to the view
            List<StudentPromotionViewModel> studentPromotionViewModels = new List<StudentPromotionViewModel>();

            // the ViewBag data we need to pass to the view as well 
            List<string> sports = new List<string>();


            // get sports taught by teacher

            var queryToGetUserSports = from sport in db.Sports
                                       where sport.IdUser == teacherId
                                       select sport.IdSport;

            var sportList = queryToGetUserSports.ToList();

            // get all the students of the teacher

            var queryToGetStudentList = from student in db.Students
                                        where queryToGetUserSports.Contains(student.IdSport)
                                        select student;

            List<Student> studentList = queryToGetStudentList.ToList();

            for (int index = 0; index < studentList.Count; index++)
            {
                int tempSportId = studentList[index].IdSport;

                var tempSportName = from sport in db.Sports
                                    where sport.IdSport == tempSportId
                                    select sport.SportName;

                // also add the sport to the ViewBag sport list, if it's not yet there

                List<string> tmp = tempSportName.ToList();
                string currentSport = tmp[0];

                if (!sports.Contains(currentSport))
                {
                    sports.Add(currentSport);
                }


                // compute attendance 
                int attendance = 10;

                studentPromotionViewModels.Add(
                          new StudentPromotionViewModel
                          {
                              Student = studentList[index],
                              SportName = currentSport,
                              Attendances = attendance,
                              IsPromoted = attendance >= 10 ? "Promoted" : "Failed",
                          }
                                    );
            }

            // List
            // ViewModel
            // Student sportName Attendances promoted

            ViewBag.SportNames = sports;

            return View(studentPromotionViewModels);
        }

        /// <summary>  
        /// Print Student report details  
        /// </summary>  
        /// <returns></returns>  
        public ActionResult PrintStudentPromotionReport(int teacherId)
        {
            var report = new Rotativa.MVC.ActionAsPdf("PrintPdfTemplate", new { teacherId = teacherId});
            return report;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
