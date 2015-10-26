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
            var form = db.Form.Find(id);
            dynamic formElementsArray = JsonConvert.DeserializeObject(form.Elements);
            DataTable table = GetResultsTable((int)id);
            List<string> ColumnNames = new List<string>();
            foreach (DataColumn col in table.Columns)
            {
                string columnName = col.ColumnName;
                foreach (var item in formElementsArray)
                {
                    string formElementId = item.id;
                    string formElementLabel = item.label;

                    if (columnName == formElementId)
                    {
                        ColumnNames.Add(formElementLabel);
                    }
                }
            }
            ViewBag.ColumnNames = ColumnNames;
            return View(table);
        }

        private DataTable GetResultsTable(int id)
        {
            var results = db.FormResults.Where(u => u.FormId == id);
            DataSet dataSet = new DataSet();
            foreach (var item in results)
            {
                StringReader stringReader = new StringReader(item.Results);
                dataSet.ReadXml(stringReader);
            }
            return dataSet.Tables[0];
        }

        // POST: Forms/ViewForm/5
        [HttpPost]
        public ActionResult SubmitForm(FormCollection collection)
        {
            string formId = collection["formID"];
            string formStorage = collection["formStorage"];
            dynamic formElementsArray = JsonConvert.DeserializeObject(collection["formElements"]);

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
                            var serverPath = Server.MapPath(FormController.rootStorageFolder + "/" + formStorage + "/" + formElementId);
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
            var a = new FormResults
            {
                Results = sb.ToString(),
                FormId = Int32.Parse(formId)
            };
            db.FormResults.Add(a);
            db.SaveChanges();

            return RedirectToAction("ThankYou");
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
