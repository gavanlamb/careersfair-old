﻿using System;
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
    public class FormController : Controller
    {
        private careersfair.DAL.FormContext db = new careersfair.DAL.FormContext();
        // GET: Forms
        public ActionResult Index(){
            return View(db.Form.ToList());
        }

        // GET: Forms/Details/5
        public ActionResult Details(int? id){
            if (id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = db.Form.Find(id);
            if (form == null){
                return HttpNotFound();
            }
            return View(form);
        }

        // GET: Forms/Create
        public ActionResult Create(){
            return View();
        }

        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,TableName,Structure,BeforeAction,AfterAction")] Form form){
            if (ModelState.IsValid){
                db.Form.Add(form);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        // GET: Forms/Edit/5
        public ActionResult Edit(int? id){
            if (id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = db.Form.Find(id);
            if (form == null){
                return HttpNotFound();
            }
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,TableName,Structure,BeforeAction,AfterAction")] Form form){
            if (ModelState.IsValid){
                db.Entry<Form>(form).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }

        // GET: Forms/Delete/5
        public ActionResult Delete(int? id){
            if (id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = db.Form.Find(id);
            if (form == null){
                return HttpNotFound();
            }
            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id){
            Form form = db.Form.Find(id);
            db.Form.Remove(form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult IsNameExists(string name){
            JsonResult ret = Json(true, JsonRequestBehavior.AllowGet);
            int result = db.Form.Where(c => c.Name.ToLower() == name.ToLower()).Count();
            if(result > 0){
                ret = Json(false, JsonRequestBehavior.AllowGet);
            }
            return ret;
        }

        protected override void Dispose(bool disposing){
            if (disposing){
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
