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
    public class DoktersController : Controller
    {
        private Context db = new Context();

        // GET: Dokters
        public ActionResult Index()
        {
            var tblDokters = db.tblDokters.Include(d => d.Locatie);
            return View(tblDokters.ToList());
        }

        // GET: Dokters/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokter dokter = db.tblDokters.Find(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        // GET: Dokters/Create
        public ActionResult Create()
        {
            ViewBag.Locatie_Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving");
            return View();
        }

        // POST: Dokters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam,IsConsultVerwachtend,Locatie_Kamer,Locatie_Afdeling,GebruikersNaam")] Dokter dokter)
        {
            if (ModelState.IsValid)
            {
                db.tblDokters.Add(dokter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Locatie_Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", dokter.Locatie_Kamer);
            return View(dokter);
        }

        // GET: Dokters/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokter dokter = db.tblDokters.Find(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            ViewBag.Locatie_Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", dokter.Locatie_Kamer);
            return View(dokter);
        }

        // POST: Dokters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam,IsConsultVerwachtend,Locatie_Kamer,Locatie_Afdeling,GebruikersNaam")] Dokter dokter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dokter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Locatie_Kamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", dokter.Locatie_Kamer);
            return View(dokter);
        }

        // GET: Dokters/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokter dokter = db.tblDokters.Find(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        // POST: Dokters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Dokter dokter = db.tblDokters.Find(id);
            db.tblDokters.Remove(dokter);
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
