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
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace careersfair.Controllers
{
    public class FormResultsController : Controller
    {
        private careersfair.DAL.FormContext db = new careersfair.DAL.FormContext();

        public ActionResult ThankYou()
        {
            return View();
        }

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


        // POST: Forms/ViewForm/5
        [HttpPost]
        public ActionResult SubmitForm(FormCollection collection)
        {
            string formId = collection["formID"];
            string formStorage = collection["formStorage"];
            dynamic formElementsArray = JsonConvert.DeserializeObject(collection["formElements"]);
            
            string rootFolder = "~/App_Data/";
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string xml = string.Empty;
            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(formStorage);
                foreach (var item in formElementsArray)
                {
                    string formElementId = item.id;
                    string formElementType = item.type;

                    if (formElementType == "file")
                    {
                        HttpPostedFileBase file = Request.Files[formElementId];
                        if (file.ContentLength > 0)
                        {
                            var random = new Random();
                            string uniqCode = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
                            var fileName = DateTime.Now.ToString("hhmmssffffff") + uniqCode + Path.GetExtension(file.FileName);
                            uniqCode = "";
                            var serverPath = Server.MapPath(rootFolder + "/" + formStorage + "/" + formElementId);
                            Directory.CreateDirectory(serverPath);
                            var path = Path.Combine(serverPath, fileName);
                            file.SaveAs(path);
                            writer.WriteElementString(formElementId, fileName);
                        }
                    }
                    else if (collection.AllKeys.Contains(formElementId))
                    {
                        writer.WriteElementString(formElementId, collection[formElementId].ToString());
                    }
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            xml = sb.ToString();
            FormResults formResults = new FormResults();
            formResults.Results = xml;
            formResults.Form = db.Form.Find(Int32.Parse(formId));

            db.FormResults.Add(formResults);
            db.SaveChanges();

            return RedirectToAction("Index");
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
