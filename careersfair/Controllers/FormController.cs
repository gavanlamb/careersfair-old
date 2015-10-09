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
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Xml;
using System.Text;
using System.IO;

namespace careersfair.Controllers
{
    /// <summary>
    /// Form object controller
    /// Created: Gavan Lamb
    /// Version: 1.0
    /// </summary
    public class FormController : Controller
    {
        private careersfair.DAL.FormContext db = new careersfair.DAL.FormContext();
        // GET: Forms
        public ActionResult Index()
        {
            return View(db.Form.ToList());
        }


        /// <summary>
        /// Returns the view to create a new form
        /// </summary>
        /// <returns>The related view to this method</returns>
        public ActionResult Create()
        {
            return View();
        }


        // POST: Forms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,HTML,Elements,Linkedin")] Form form)
        {
            if (ModelState.IsValid)
            {
                string rootFolder = "~/App_Data/";
                string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string formStorage = form.Name;
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                formStorage = rgx.Replace(formStorage, "");

                var random = new Random();
                formStorage += new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

                dynamic formElementsArray = JsonConvert.DeserializeObject(form.Elements);
                foreach (var item in formElementsArray)
                {
                    string formElementType = item.type;
                    if (formElementType == "file")
                    {
                        string formElementId = item.id;
                        var path = Server.MapPath(rootFolder + "/" + formStorage + "/" + formElementId);
                        Directory.CreateDirectory(path);
                    }
                }
                form.Storage = formStorage;
                form.Enabled = true;
                db.Form.Add(form);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(form);
        }


        // GET: Forms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = db.Form.Find(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return PartialView("Delete", form);
        }


        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Form form = db.Form.Find(id);
            db.Form.Remove(form);
            db.SaveChanges();
            return Json(new { success = true });
        }


        // GET: Forms/EnableDisable/5
        public ActionResult EnableDisable(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = db.Form.Find(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return PartialView("EnableDisable", form);
        }


        // POST: Forms/EnableDisable/5
        [HttpPost, ActionName("EnableDisable")]
        [ValidateAntiForgeryToken]
        public ActionResult EnableDisableConfirmed(int id)
        {
            Form form = db.Form.Find(id);
            if (form.Enabled)
            {
                form.Enabled = false;
            }
            else
            {
                form.Enabled = true;
            }
            db.SaveChanges();
            return Json(new { success = true });
        }


        // GET: Forms/ViewForm/5
        public ActionResult ViewForm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Form form = db.Form.Find(id);
            if (form == null)
            {
                return HttpNotFound();
            }
            return View(form);
        }
        

        /// <summary>
        /// Called from the model in order to validate whether the name is unique 
        /// </summary>
        /// <param name="name">Name of the form to check</param>
        /// <returns>JSON boolean - true if the name has been taken</returns>
        [HttpPost]
        public JsonResult IsNameExists(string name)
        {
            string nameLoc = name.Trim();
            JsonResult ret = Json(true, JsonRequestBehavior.AllowGet);
            int result = db.Form.Where(c => c.Name.ToLower() == nameLoc.ToLower()).Count();
            if (result > 0)
            {
                ret = Json(false, JsonRequestBehavior.AllowGet);
            }
            return ret;
        }


        /// <summary>
        /// Dispose the database connection
        /// </summary>
        /// <param name="disposing"></param>
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
