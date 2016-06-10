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
    public class ExceptionLoggersController : Controller
    {
        private Context db = new Context();

        // GET: ExceptionLoggers
        public ActionResult Index()
        {
            return View(db.tblExceptionLogger.ToList());
        }

        // GET: ExceptionLoggers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExceptionLogger exceptionLogger = db.tblExceptionLogger.Find(id);
            if (exceptionLogger == null)
            {
                return HttpNotFound();
            }
            return View(exceptionLogger);
        }

        // GET: ExceptionLoggers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExceptionLoggers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExceptionMessage,ExceptionStackTrace,LogTime")] ExceptionLogger exceptionLogger)
        {
            if (ModelState.IsValid)
            {
                db.tblExceptionLogger.Add(exceptionLogger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exceptionLogger);
        }

        // GET: ExceptionLoggers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExceptionLogger exceptionLogger = db.tblExceptionLogger.Find(id);
            if (exceptionLogger == null)
            {
                return HttpNotFound();
            }
            return View(exceptionLogger);
        }

        // POST: ExceptionLoggers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExceptionMessage,ExceptionStackTrace,LogTime")] ExceptionLogger exceptionLogger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exceptionLogger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exceptionLogger);
        }

        // GET: ExceptionLoggers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExceptionLogger exceptionLogger = db.tblExceptionLogger.Find(id);
            if (exceptionLogger == null)
            {
                return HttpNotFound();
            }
            return View(exceptionLogger);
        }

        // POST: ExceptionLoggers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExceptionLogger exceptionLogger = db.tblExceptionLogger.Find(id);
            db.tblExceptionLogger.Remove(exceptionLogger);
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
