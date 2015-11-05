using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CareersFair.DAL;
using CareersFair.Models;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace CareersFair.Controllers
{
    public class FormResultsController : Controller
    {
        private CareersFair.DAL.FormContext db = new CareersFair.DAL.FormContext();

        public ActionResult ThankYou()
        {
            return View();
        }

        // GET: FormResults/Details/5
        public ActionResult Data(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var form = db.Form.Find(id);
            dynamic formElementsArray = JsonConvert.DeserializeObject(form.Elements);
            DataTable dt = GetResultsTable((int)id);
            List<string> ColumnNames = new List<string>();
            foreach (DataColumn col in dt.Columns)
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
            ViewBag.FormName = form.Name;
            return View(dt);
        }

        // GET: FormResults/Details/5
        public ActionResult DownloadData(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var form = db.Form.Find(id);

            Regex rgx = new Regex("[^a-zA-Z0-9]");
            string formName = rgx.Replace(form.Name, "");
            string formElements = form.Elements;
            string formStorage = form.Storage;

            var archive = Server.MapPath("~/archive.zip");
            var temp = Server.MapPath("~/temp");
            var formStorageDirectoy = Server.MapPath(FormController.rootStorageFolder + formStorage);
            var dataFile = Path.Combine(temp, formName + ".csv");

            DataTable dt = GetResultsTable((int)id);
            IEnumerable<string> columnNames = new string[] { };
            dynamic formElementsArray = JsonConvert.DeserializeObject(formElements);

            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }
            if (Directory.Exists(temp)) {
                Directory.Delete(temp, true);
            }
            Directory.CreateDirectory(temp);
            StreamWriter sw = new StreamWriter(dataFile);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string columnName = dt.Columns[i].ColumnName;
                foreach (var item in formElementsArray)
                {
                    string formElementId = item.id;
                    string formElementLabel = item.label;
                    if (columnName == formElementId)
                    {
                        string value = formElementLabel;
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(value);
                        }
                        if (i < dt.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                }
            }
            sw.Write(sw.NewLine); foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dt.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
            foreach (var item in formElementsArray)
            {
                string formElementId = item.id;
                string formElementType = item.type;
                string formElementLabel = item.label;
                formElementLabel = rgx.Replace(formElementLabel, "");
                if (formElementType.ToLower() == "file")
                {
                    var formElementDirector = Path.Combine(formStorageDirectoy, formElementId);
                    if (Directory.Exists(formElementDirector))
                    {
                        if (Directory.EnumerateFiles(formElementDirector, "*", SearchOption.AllDirectories).Any())
                        {
                            foreach (var file in Directory.GetFiles(formElementDirector))
                            {
                                var tempElementFolder = Path.Combine(temp, formElementLabel + "_files");
                                Directory.CreateDirectory(tempElementFolder);
                                System.IO.File.Copy(file, Path.Combine(tempElementFolder, Path.GetFileName(file)), true);
                            }
                        }
                    }
                }
            }
            ZipFile.CreateFromDirectory(temp, archive);
            if (Directory.Exists(temp))
            {
                Directory.Delete(temp, true);
            }
            return File(archive, "application/zip", formName + "_data.zip");
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
                        else {
                            writer.WriteElementString(formElementId,null);
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
