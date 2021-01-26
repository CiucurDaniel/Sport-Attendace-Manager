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
        public ActionResult PromotionStatus(int teacherID)
        {
            // here you make a get with teacher ID
            // and then we will display a list with teachers sports

            List<Sport> sportsOwnedByTeacher = new List<Sport>();


            /*IEnumerable<Student> students = from student in db.Students
                where student.IdSport == sportId
                select student; */

            return Content("Yayy!");
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
