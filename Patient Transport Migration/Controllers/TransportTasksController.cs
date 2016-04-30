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
    public class TransportTasksController : Controller
    {
        private MSSQLContext db = new MSSQLContext();

        // GET: TransportTasks
        public ActionResult Index()
        {
            return View(db.TransportTasks.ToList());
        }

        // GET: TransportTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportTask transportTask = db.TransportTasks.Find(id);
            if (transportTask == null)
            {
                return HttpNotFound();
            }
            return View(transportTask);
        }

        // GET: TransportTasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransportTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,FK_Request_Type,Fk_Diagnostische_Onderzoeken,FK_Request_Status,DateCreated,DateComplete,Notes,EmployeeAssigned,HighPriority,Patient,Notities,Van,Naar,date_created,date_complete,datum_van_aanvraag,datum_behandeld,huidige_klachten,aan_dokter,andere_notas,aanvragende_geneesheer,patient_wordt_behandeld_voor,bevindingen_en_advies,relevante_klinische_inlichtingen,diagnostische_vraagstelling,transport_te_voet,transport_rolstoel,transport_bed,zwanger,diabeet,implantaat,nierinsufficientie,allergie,andere")] TransportTask transportTask)
        {
            if (ModelState.IsValid)
            {
                db.TransportTasks.Add(transportTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transportTask);
        }

        // GET: TransportTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportTask transportTask = db.TransportTasks.Find(id);
            if (transportTask == null)
            {
                return HttpNotFound();
            }
            return View(transportTask);
        }

        // POST: TransportTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,FK_Request_Type,Fk_Diagnostische_Onderzoeken,FK_Request_Status,DateCreated,DateComplete,Notes,EmployeeAssigned,HighPriority,Patient,Notities,Van,Naar,date_created,date_complete,datum_van_aanvraag,datum_behandeld,huidige_klachten,aan_dokter,andere_notas,aanvragende_geneesheer,patient_wordt_behandeld_voor,bevindingen_en_advies,relevante_klinische_inlichtingen,diagnostische_vraagstelling,transport_te_voet,transport_rolstoel,transport_bed,zwanger,diabeet,implantaat,nierinsufficientie,allergie,andere")] TransportTask transportTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transportTask);
        }

        // GET: TransportTasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportTask transportTask = db.TransportTasks.Find(id);
            if (transportTask == null)
            {
                return HttpNotFound();
            }
            return View(transportTask);
        }

        // POST: TransportTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransportTask transportTask = db.TransportTasks.Find(id);
            db.TransportTasks.Remove(transportTask);
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
