using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;


namespace careersfair.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Reset()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                if (user.IsValid(user.UserName, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, user.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ResetPassword()
        {
             // Gmail Address from where you send the mail
            String fromAddress = "sai.ancha194@gmail.com";
            // any address where the email will be sending
            String toAddress = "sai.ancha194gmail.com";
            //Password of your gmail address
            const string fromPassword = "lollollol1";
            // Passing the values and make a email formate to display
            string subject = "Fiserv Admin password Reset";
            string body = "Your Password has been Reset. Your new password is fiserv.";

            // smtp settings
            var smtp = new System.Net.Mail.SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                smtp.Timeout = 20000;
            }
            // Passing values to smtp object
            smtp.Send("sai.ancha194@gmail.com", "sai.ancha194@gmail.com", subject, body);

           // string connectionString = @"Data Source=(LocalDB)\v11.0;Initial Catalog=Database1;Integrated Security=True;";
            String connString = (@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\GitHub\careersfair\careersfair\App_Data\Database1.mdf;Integrated Security=True");
            SqlConnection connection = new SqlConnection( connString);
            SqlCommand command = new SqlCommand("SELECT * FROM System_Users", connection);
            command.Connection.Open();

            string querystr = "UPDATE System_Users SET Password='9918287aee6b3451d3d81429658a1a0bb5a30971'";
            SqlCommand query = new SqlCommand(querystr, connection);
            query.ExecuteNonQuery();
            command.Connection.Close();
            return View();
        }
       
    }
}