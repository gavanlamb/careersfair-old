using careersfair.DAL;
using careersfair.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.IO.Compression;

namespace careersfair.Controllers
{
    /// <summary>
    /// Form object controller
    /// Created: Gavan Lamb
    /// Version: 1.0
    /// </summary
    public class FormController : Controller
    {
        public static string rootStorageFolder = "~/App_Data/";
        private careersfair.DAL.FormContext db = new careersfair.DAL.FormContext();

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
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateConfirmed([Bind(Include = "Name,HTML,Elements,Linkedin")] Form form)
        {
            if (ModelState.IsValid)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9]");
                string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string formStorage = form.Name;
                var random = new Random();
                formStorage = rgx.Replace(formStorage, "");
                form.Storage = "fs" + formStorage + new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
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
            string formStorage = form.Storage;
            string radioValue = Request.Form["deleteradio"];
            if (radioValue == "formdata" || radioValue == "data" || radioValue == null && form.FormResults.Count == 0)
            {
                var path = Server.MapPath(rootStorageFolder + formStorage);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                IEnumerable<FormResults> formResults = db.FormResults.Where(a => a.FormId.Equals(id));
                foreach (var item in formResults)
                {
                    db.FormResults.Remove(item);
                }
                if (radioValue == "formdata" || radioValue == null && form.FormResults.Count == 0)
                {
                    db.Form.Remove(form);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("Delete", form);
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
            if (form != null)
            {
                form.Enabled = (form.Enabled) ? false : true;
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // GET: Forms
        public ActionResult Index()
        {
            return View(db.Form.ToList());
        }

        public FileResult GetQRCode(int id)
        {
            Form form = db.Form.Find(id);
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            QRCodeEncoder encoder = new QRCodeEncoder();

            string formName = rgx.Replace(form.Name, "");
            string formStorage = form.Storage;
            string url = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/Form/ViewForm/" + id;

            var archive = Server.MapPath("~/archive.zip");
            var temp = Server.MapPath("~/temp");

            if (System.IO.File.Exists(archive))
            {
                System.IO.File.Delete(archive);
            }
            if (Directory.Exists(temp))
            {
                Directory.Delete(temp, true);
            }
            Directory.CreateDirectory(temp);
            for (int i = 1; i < 9; i++)
            {
                encoder.QRCodeScale = ((int)Math.Pow(2.0, (double)i));
                Bitmap img = encoder.Encode(url);
                img.Save(Server.MapPath("~/temp/qrcode" + i + ".jpg"), ImageFormat.Jpeg);
            }
            ZipFile.CreateFromDirectory(temp, archive);
            return File(archive, "application/zip", formName + "_qrcode.zip");
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
