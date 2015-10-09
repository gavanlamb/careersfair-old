using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using careersfair.DAL;
using careersfair.Models;

namespace careersfair.Controllers
{
    public class FormResultsController : Controller
    {
        private careersfair.DAL.FormContext db = new careersfair.DAL.FormContext();

        // GET: FormResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormResults formResults = db.FormResults.Find(id);
            if (formResults == null)
            {
                return HttpNotFound();
            }
            return View(formResults);
        }

        // POST: FormResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Results")] FormResults formResults)
        {
            if (ModelState.IsValid)
            {
                db.FormResults.Add(formResults);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(formResults);
        }

        // GET: FormResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormResults formResults = db.FormResults.Find(id);
            if (formResults == null)
            {
                return HttpNotFound();
            }
            return View(formResults);
        }

        // POST: FormResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormResults formResults = db.FormResults.Find(id);
            db.FormResults.Remove(formResults);
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
