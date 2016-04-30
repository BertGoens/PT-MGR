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
    public class RequestStatusController : Controller
    {
        private MSSQLContext db = new MSSQLContext();

        // GET: RequestStatus
        public ActionResult Index()
        {
            return View(db.RequestStatus.ToList());
        }

        // GET: RequestStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestStatus requestStatus = db.RequestStatus.Find(id);
            if (requestStatus == null)
            {
                return HttpNotFound();
            }
            return View(requestStatus);
        }

        // GET: RequestStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pk_Id,Fk_Request_Type,Description,Next")] RequestStatus requestStatus)
        {
            if (ModelState.IsValid)
            {
                db.RequestStatus.Add(requestStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(requestStatus);
        }

        // GET: RequestStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestStatus requestStatus = db.RequestStatus.Find(id);
            if (requestStatus == null)
            {
                return HttpNotFound();
            }
            return View(requestStatus);
        }

        // POST: RequestStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pk_Id,Fk_Request_Type,Description,Next")] RequestStatus requestStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestStatus);
        }

        // GET: RequestStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestStatus requestStatus = db.RequestStatus.Find(id);
            if (requestStatus == null)
            {
                return HttpNotFound();
            }
            return View(requestStatus);
        }

        // POST: RequestStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestStatus requestStatus = db.RequestStatus.Find(id);
            db.RequestStatus.Remove(requestStatus);
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
