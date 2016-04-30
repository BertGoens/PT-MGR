using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Patient_Transport_Migration.Models;
using Patient_Transport_Migration.Models.DAL;

namespace Patinet_Transport_Migration.Controllers {
    public class DoktersController : Controller
    {
        private MSSQLContext db = new MSSQLContext();

        // GET: Dokters
        public ActionResult Index()
        {
            return View(db.Docters.ToList());
        }

        // GET: Dokters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokter dokter = db.Docters.Find(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        // GET: Dokters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dokters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] Dokter dokter)
        {
            if (ModelState.IsValid)
            {
                db.Docters.Add(dokter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dokter);
        }

        // GET: Dokters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokter dokter = db.Docters.Find(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        // POST: Dokters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] Dokter dokter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dokter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dokter);
        }

        // GET: Dokters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dokter dokter = db.Docters.Find(id);
            if (dokter == null)
            {
                return HttpNotFound();
            }
            return View(dokter);
        }

        // POST: Dokters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dokter dokter = db.Docters.Find(id);
            db.Docters.Remove(dokter);
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
