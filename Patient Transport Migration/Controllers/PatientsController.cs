using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models.DAL;
using Patient_Transport_Migration.Models.POCO;

namespace Patient_Transport_Migration.Controllers
{
    public class PatientsController : Controller
    {
        private Context db = new Context();

        // GET: Patients
        public ActionResult Index()
        {
            var tblPatienten = db.tblPatienten.Include(p => p.Locatie);
            return View(tblPatienten.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.tblPatienten.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            ViewBag.Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientId,PatientVisit,Voornaam,Achternaam,Geboortedatum,Geslacht,Afdeling,Kamer,BedNr")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.tblPatienten.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", patient.Kamer);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.tblPatienten.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", patient.Kamer);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientId,PatientVisit,Voornaam,Achternaam,Geboortedatum,Geslacht,Afdeling,Kamer,BedNr")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", patient.Kamer);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.tblPatienten.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Patient patient = db.tblPatienten.Find(id);
            db.tblPatienten.Remove(patient);
            db.SaveChanges();
            return RedirectToAction("Index");
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
