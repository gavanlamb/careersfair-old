using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace careersfair.Models
{
    public class Reset
    {
        protected void SendMail()
        {
            // Gmail Address from where you send the mail
            var fromAddress = "sai.ancha194@gmail.com";
            // any address where the email will be sending
            var toAddress = "sai.ancha194gmail.com";
            //Password of your gmail address
            const string fromPassword = "lollollol1";
            // Passing the values and make a email formate to display
            string subject = "Fiserv Admin password Reset";
            string body = "From: Sai";

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
            smtp.Send(fromAddress, toAddress, subject, body);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                //here on button click what will done 
                SendMail();

            }
            catch (Exception) { }
        }
    }
}