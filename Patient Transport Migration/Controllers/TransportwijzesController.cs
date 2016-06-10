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
    public class TransportwijzesController : Controller
    {
        private Context db = new Context();

        // GET: Transportwijzes
        public ActionResult Index()
        {
            return View(db.tblTransportwijzes.ToList());
        }

        // GET: Transportwijzes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportwijze transportwijze = db.tblTransportwijzes.Find(id);
            if (transportwijze == null)
            {
                return HttpNotFound();
            }
            return View(transportwijze);
        }

        // GET: Transportwijzes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transportwijzes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Omschrijving")] Transportwijze transportwijze)
        {
            if (ModelState.IsValid)
            {
                db.tblTransportwijzes.Add(transportwijze);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportwijze);
        }

        // GET: Transportwijzes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportwijze transportwijze = db.tblTransportwijzes.Find(id);
            if (transportwijze == null)
            {
                return HttpNotFound();
            }
            return View(transportwijze);
        }

        // POST: Transportwijzes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Omschrijving")] Transportwijze transportwijze)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportwijze).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportwijze);
        }

        // GET: Transportwijzes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transportwijze transportwijze = db.tblTransportwijzes.Find(id);
            if (transportwijze == null)
            {
                return HttpNotFound();
            }
            return View(transportwijze);
        }

        // POST: Transportwijzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transportwijze transportwijze = db.tblTransportwijzes.Find(id);
            db.tblTransportwijzes.Remove(transportwijze);
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
