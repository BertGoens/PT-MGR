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
    public class TransportTaaksController : Controller
    {
        private Context db = new Context();

        // GET: TransportTaaks
        public ActionResult Index()
        {
            var tblTransportTaken = db.tblTransportTaken.Include(t => t.Aanvraag).Include(t => t.DokterEind).Include(t => t.LocatieEind).Include(t => t.LocatieStart).Include(t => t.TransportWerknemer);
            return View(tblTransportTaken.ToList());
        }

        // GET: TransportTaaks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportTaak transportTaak = db.tblTransportTaken.Find(id);
            if (transportTaak == null)
            {
                return HttpNotFound();
            }
            return View(transportTaak);
        }

        // GET: TransportTaaks/Create
        public ActionResult Create()
        {
            ViewBag.AanvraagId = new SelectList(db.tblAanvragen, "Id", "AanvraagDoor");
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam");
            ViewBag.LocatieEindKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving");
            ViewBag.LocatieStartKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving");
            ViewBag.TransportWerknemerId = new SelectList(db.tblTransportWerknemers, "Gebruikersnaam", "Voornaam");
            return View();
        }

        // POST: TransportTaaks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocatieStartKamer,LocatieStartAfdelingId,LocatieEindKamer,LocatieEindAfdelingId,DokterId,TransportNotities,IsPrioriteitHoog,DatumGemaakt,DatumCompleet,AanvraagId,TransportGestart,TaakWachtrijNummer,TransportWerknemerId")] TransportTaak transportTaak)
        {
            if (ModelState.IsValid)
            {
                db.tblTransportTaken.Add(transportTaak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AanvraagId = new SelectList(db.tblAanvragen, "Id", "AanvraagDoor", transportTaak.AanvraagId);
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam", transportTaak.DokterId);
            ViewBag.LocatieEindKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", transportTaak.LocatieEindKamer);
            ViewBag.LocatieStartKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", transportTaak.LocatieStartKamer);
            ViewBag.TransportWerknemerId = new SelectList(db.tblTransportWerknemers, "Gebruikersnaam", "Voornaam", transportTaak.TransportWerknemerId);
            return View(transportTaak);
        }

        // GET: TransportTaaks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportTaak transportTaak = db.tblTransportTaken.Find(id);
            if (transportTaak == null)
            {
                return HttpNotFound();
            }
            ViewBag.AanvraagId = new SelectList(db.tblAanvragen, "Id", "AanvraagDoor", transportTaak.AanvraagId);
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam", transportTaak.DokterId);
            ViewBag.LocatieEindKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", transportTaak.LocatieEindKamer);
            ViewBag.LocatieStartKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", transportTaak.LocatieStartKamer);
            ViewBag.TransportWerknemerId = new SelectList(db.tblTransportWerknemers, "Gebruikersnaam", "Voornaam", transportTaak.TransportWerknemerId);
            return View(transportTaak);
        }

        // POST: TransportTaaks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocatieStartKamer,LocatieStartAfdelingId,LocatieEindKamer,LocatieEindAfdelingId,DokterId,TransportNotities,IsPrioriteitHoog,DatumGemaakt,DatumCompleet,AanvraagId,TransportGestart,TaakWachtrijNummer,TransportWerknemerId")] TransportTaak transportTaak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportTaak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AanvraagId = new SelectList(db.tblAanvragen, "Id", "AanvraagDoor", transportTaak.AanvraagId);
            ViewBag.DokterId = new SelectList(db.tblDokters, "Id", "Naam", transportTaak.DokterId);
            ViewBag.LocatieEindKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", transportTaak.LocatieEindKamer);
            ViewBag.LocatieStartKamer = new SelectList(db.tblLocaties, "Kamer", "Omschrijving", transportTaak.LocatieStartKamer);
            ViewBag.TransportWerknemerId = new SelectList(db.tblTransportWerknemers, "Gebruikersnaam", "Voornaam", transportTaak.TransportWerknemerId);
            return View(transportTaak);
        }

        // GET: TransportTaaks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportTaak transportTaak = db.tblTransportTaken.Find(id);
            if (transportTaak == null)
            {
                return HttpNotFound();
            }
            return View(transportTaak);
        }

        // POST: TransportTaaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportTaak transportTaak = db.tblTransportTaken.Find(id);
            db.tblTransportTaken.Remove(transportTaak);
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
