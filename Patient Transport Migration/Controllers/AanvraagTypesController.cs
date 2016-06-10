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
    public class AanvraagTypesController : Controller
    {
        private Context db = new Context();

        // GET: AanvraagTypes
        public ActionResult Index()
        {
            return View(db.tblAanvraagTypes.ToList());
        }

        // GET: AanvraagTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AanvraagType aanvraagType = db.tblAanvraagTypes.Find(id);
            if (aanvraagType == null)
            {
                return HttpNotFound();
            }
            return View(aanvraagType);
        }

        // GET: AanvraagTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AanvraagTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Omschrijving,DatumGemaakt,Include_Transportwijze,Include_Patient,Include_AanDokter,Include_Radiologie")] AanvraagType aanvraagType)
        {
            if (ModelState.IsValid)
            {
                db.tblAanvraagTypes.Add(aanvraagType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aanvraagType);
        }

        // GET: AanvraagTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AanvraagType aanvraagType = db.tblAanvraagTypes.Find(id);
            if (aanvraagType == null)
            {
                return HttpNotFound();
            }
            return View(aanvraagType);
        }

        // POST: AanvraagTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Omschrijving,DatumGemaakt,Include_Transportwijze,Include_Patient,Include_AanDokter,Include_Radiologie")] AanvraagType aanvraagType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aanvraagType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aanvraagType);
        }

        // GET: AanvraagTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AanvraagType aanvraagType = db.tblAanvraagTypes.Find(id);
            if (aanvraagType == null)
            {
                return HttpNotFound();
            }
            return View(aanvraagType);
        }

        // POST: AanvraagTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AanvraagType aanvraagType = db.tblAanvraagTypes.Find(id);
            db.tblAanvraagTypes.Remove(aanvraagType);
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
