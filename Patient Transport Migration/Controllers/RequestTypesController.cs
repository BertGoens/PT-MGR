using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patient_Transport_Migration.Controllers
{
    public class RequestTypesController : Controller
    {
        private MSSQLContext db = new MSSQLContext();

        // GET: RequestTypes
        public ActionResult Index()
        {
            return View(db.RequestTypes.ToList());
        }

        // GET: RequestTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = db.RequestTypes.Find(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // GET: RequestTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,DateCreated,Include_datum_van_aanvraag,Include_datum_behandeld,Include_huidige_klachten,Include_aan_dokter,Include_andere_notas,Include_aanvragende_geneesheer,Include_patient_wordt_behandeld_voor,Include_bevindingen_en_advies,Include_relevante_klinische_inlichtingen,Include_diagnostische_vraagstelling,Include_transport_te_voet,Include_transport_rolstoel,Include_transport_bed,Include_zwanger,Include_diabeet,Include_implantaat,Include_nierinsufficientie,Include_allergie,Include_andere")] RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                db.RequestTypes.Add(requestType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requestType);
        }

        // GET: RequestTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = db.RequestTypes.Find(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // POST: RequestTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,DateCreated,Include_datum_van_aanvraag,Include_datum_behandeld,Include_huidige_klachten,Include_aan_dokter,Include_andere_notas,Include_aanvragende_geneesheer,Include_patient_wordt_behandeld_voor,Include_bevindingen_en_advies,Include_relevante_klinische_inlichtingen,Include_diagnostische_vraagstelling,Include_transport_te_voet,Include_transport_rolstoel,Include_transport_bed,Include_zwanger,Include_diabeet,Include_implantaat,Include_nierinsufficientie,Include_allergie,Include_andere")] RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestType);
        }

        // GET: RequestTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = db.RequestTypes.Find(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // POST: RequestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestType requestType = db.RequestTypes.Find(id);
            db.RequestTypes.Remove(requestType);
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
