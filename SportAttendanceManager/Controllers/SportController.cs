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

namespace SportAttendanceSystem.Controllers
{
    public class SportController : Controller
    {
        private SportAttendanceManagerContext db = new SportAttendanceManagerContext();

        // GET: Sport
        public ActionResult Index(int idTeacher)
        {
            //var sports = db.Sports.Include(s => s.User);
            //return View(sports.ToList());

            //List<Sport> sportList = new List<Sport>();

            IEnumerable<Sport> query = from sport in db.Sports
                                       where sport.IdUser == idTeacher
                                       select sport;


            List<Sport> sportsOfCurrentTeacher = new List<Sport>();
            //sportsOfCurrentTeacher = query.ToList();
            foreach (Sport sport in query)
            {
                sportsOfCurrentTeacher.Add(sport);
            }

            return View(sportsOfCurrentTeacher);
        }

        // GET: Sport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sports.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // GET: Sport/Create
        public ActionResult Create()
        {
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FirstName");
            return View();
        }

        // POST: Sport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSport,SportName,Hour,Minute,Description,DayOfWeek,IdUser")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                db.Sports.Add(sport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FirstName", sport.IdUser);
            return View(sport);
        }

        // GET: Sport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sports.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FirstName", sport.IdUser);
            return View(sport);
        }

        // POST: Sport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSport,SportName,Hour,Minute,Description,DayOfWeek,IdUser")] Sport sport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FirstName", sport.IdUser);
            return View(sport);
        }

        // GET: Sport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sport sport = db.Sports.Find(id);
            if (sport == null)
            {
                return HttpNotFound();
            }
            return View(sport);
        }

        // POST: Sport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sport sport = db.Sports.Find(id);
            db.Sports.Remove(sport);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET Sport/SportsOfTeacher/
        public ActionResult SportsOfTeacher(int idTeacher)
        {
            IEnumerable<Sport> query = from sport in db.Sports
                                       where sport.IdUser == idTeacher
                                       select sport;


            List<Sport> sportsOfCurrentTeacher = new List<Sport>();
            //sportsOfCurrentTeacher = query.ToList();
            foreach (Sport sport in query)
            {
                sportsOfCurrentTeacher.Add(sport);
            }

            return View(sportsOfCurrentTeacher);
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
