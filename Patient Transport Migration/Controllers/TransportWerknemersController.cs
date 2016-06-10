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
    public class TransportWerknemersController : Controller
    {
        private Context db = new Context();

        // GET: TransportWerknemers
        public ActionResult Index()
        {
            return View(db.tblTransportWerknemers.ToList());
        }

        // GET: TransportWerknemers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportWerknemer transportWerknemer = db.tblTransportWerknemers.Find(id);
            if (transportWerknemer == null)
            {
                return HttpNotFound();
            }
            return View(transportWerknemer);
        }

        // GET: TransportWerknemers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransportWerknemers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Gebruikersnaam,Voornaam,Achternaam")] TransportWerknemer transportWerknemer)
        {
            if (ModelState.IsValid)
            {
                db.tblTransportWerknemers.Add(transportWerknemer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportWerknemer);
        }

        // GET: TransportWerknemers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportWerknemer transportWerknemer = db.tblTransportWerknemers.Find(id);
            if (transportWerknemer == null)
            {
                return HttpNotFound();
            }
            return View(transportWerknemer);
        }

        // POST: TransportWerknemers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Gebruikersnaam,Voornaam,Achternaam")] TransportWerknemer transportWerknemer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportWerknemer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportWerknemer);
        }

        // GET: TransportWerknemers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportWerknemer transportWerknemer = db.tblTransportWerknemers.Find(id);
            if (transportWerknemer == null)
            {
                return HttpNotFound();
            }
            return View(transportWerknemer);
        }

        // POST: TransportWerknemers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TransportWerknemer transportWerknemer = db.tblTransportWerknemers.Find(id);
            db.tblTransportWerknemers.Remove(transportWerknemer);
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
