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
    public class AanvraagsController : Controller
    {
        private Context db = new Context();

        // GET: Aanvraags
        public ActionResult Index()
        {
            var tblAanvragen = db.tblAanvragen.Include(a => a.AanDokter).Include(a => a.AanvraagType).Include(a => a.Patient).Include(a => a.PatientBij).Include(a => a.Transportwijze);
            return View(tblAanvragen.ToList());
        }

        // GET: Aanvraags/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aanvraag aanvraag = db.tblAanvragen.Find(id);
            if (aanvraag == null)
            {
                return HttpNotFound();
            }
            return View(aanvraag);
        }

        // GET: Aanvraags/Create
        public ActionResult Create()
        {
            ViewBag.AanDokterId = new SelectList(db.tblDokters, "Id", "Naam");
            ViewBag.AanvraagTypeId = new SelectList(db.tblAanvraagTypes, "Id", "Omschrijving");
            ViewBag.PatientId = new SelectList(db.tblPatienten, "PatientId", "Voornaam");
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam");
            ViewBag.TransportwijzeId = new SelectList(db.tblTransportwijzes, "Id", "Omschrijving");
            return View();
        }

        // POST: Aanvraags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatumAanvraag,DatumCompleet,AanvraagTypeId,AanvraagDoor,Omschrijving,DokterId,PatientId,PatientVisit,AanDokterId,DokterOntslagen,CT,CT_Ontslagen,NMR,NMR_Ontslagen,RX,RX_Ontslagen,Echografie,Echografie_Ontslagen,TransportwijzeId")] Aanvraag aanvraag)
        {
            if (ModelState.IsValid)
            {
                db.tblAanvragen.Add(aanvraag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AanDokterId = new SelectList(db.tblDokters, "Id", "Naam", aanvraag.AanDokterId);
            ViewBag.AanvraagTypeId = new SelectList(db.tblAanvraagTypes, "Id", "Omschrijving", aanvraag.AanvraagTypeId);
            ViewBag.PatientId = new SelectList(db.tblPatienten, "PatientId", "Voornaam", aanvraag.PatientId);
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam", aanvraag.DokterId);
            ViewBag.TransportwijzeId = new SelectList(db.tblTransportwijzes, "Id", "Omschrijving", aanvraag.TransportwijzeId);
            return View(aanvraag);
        }

        // GET: Aanvraags/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aanvraag aanvraag = db.tblAanvragen.Find(id);
            if (aanvraag == null)
            {
                return HttpNotFound();
            }
            ViewBag.AanDokterId = new SelectList(db.tblDokters, "Id", "Naam", aanvraag.AanDokterId);
            ViewBag.AanvraagTypeId = new SelectList(db.tblAanvraagTypes, "Id", "Omschrijving", aanvraag.AanvraagTypeId);
            ViewBag.PatientId = new SelectList(db.tblPatienten, "PatientId", "Voornaam", aanvraag.PatientId);
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam", aanvraag.DokterId);
            ViewBag.TransportwijzeId = new SelectList(db.tblTransportwijzes, "Id", "Omschrijving", aanvraag.TransportwijzeId);
            return View(aanvraag);
        }

        // POST: Aanvraags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatumAanvraag,DatumCompleet,AanvraagTypeId,AanvraagDoor,Omschrijving,DokterId,PatientId,PatientVisit,AanDokterId,DokterOntslagen,CT,CT_Ontslagen,NMR,NMR_Ontslagen,RX,RX_Ontslagen,Echografie,Echografie_Ontslagen,TransportwijzeId")] Aanvraag aanvraag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aanvraag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AanDokterId = new SelectList(db.tblDokters, "Id", "Naam", aanvraag.AanDokterId);
            ViewBag.AanvraagTypeId = new SelectList(db.tblAanvraagTypes, "Id", "Omschrijving", aanvraag.AanvraagTypeId);
            ViewBag.PatientId = new SelectList(db.tblPatienten, "PatientId", "Voornaam", aanvraag.PatientId);
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam", aanvraag.DokterId);
            ViewBag.TransportwijzeId = new SelectList(db.tblTransportwijzes, "Id", "Omschrijving", aanvraag.TransportwijzeId);
            return View(aanvraag);
        }

        // GET: Aanvraags/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aanvraag aanvraag = db.tblAanvragen.Find(id);
            if (aanvraag == null)
            {
                return HttpNotFound();
            }
            return View(aanvraag);
        }

        // POST: Aanvraags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Aanvraag aanvraag = db.tblAanvragen.Find(id);
            db.tblAanvragen.Remove(aanvraag);
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
