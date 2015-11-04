using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using careersfair.Controllers;


namespace careersfair.Models
{
    public class ChangePassword
    {
        //Change password
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }



        public Boolean Successful(String oldPassword, String newPassword)
        {
            String getPassword = "";
            OldPassword = Helpers.SHA1.Encode(OldPassword);
            String connString = (@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\GitHub\careersfair\careersfair\App_Data\Database1.mdf;Integrated Security=True");
            SqlConnection connection = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT [Password]  FROM System_Users", connection);
            command.Connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                getPassword = reader.GetString(0);

            }
            command.Connection.Close();
            if (getPassword == OldPassword)
            {
                NewPassword = Helpers.SHA1.Encode(NewPassword);
                string querystr = "UPDATE System_Users SET Password= @newPassword";
                SqlCommand query = new SqlCommand(querystr, connection);
                command.Connection.Open();
                query.Parameters.AddWithValue("@newPassword", NewPassword);
                query.ExecuteNonQuery();
                command.Connection.Close();
                return true;

            }
            else return false;



        }
    }
}
        //public bool IsValid(string _oldPassword, string _newPassword)
        //{
        //    using (var cn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\GitHub\careersfair\careersfair\App_Data\Database1.mdf;Integrated Security=True"))
        //    {
        //        string _sql = @"SELECT [Password] FROM [dbo].[System_Users] WHERE [Password] = @u";
        //        var cmd = new SqlCommand(_sql, cn);
        //        cmd.Parameters
        //            .Add(new SqlParameter("@u", SqlDbType.NVarChar))
        //            .Value = Helpers.SHA1.Encode(_oldPassword);
                
        //        cn.Open();
        //        var reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            reader.Dispose();
        //            cmd.Dispose();
        //            cn.Close();
        //           _newPassword =  Helpers.SHA1.Encode(_newPassword);
        //            String connString = (@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\GitHub\careersfair\careersfair\App_Data\Database1.mdf;Integrated Security=True");
        //            SqlConnection connection = new SqlConnection(connString);
        //            SqlCommand command = new SqlCommand("SELECT [Password] FROM System_Users WHERE [Password] = @u", connection);
        //            command.Connection.Open();

        //            string querystr = "UPDATE System_Users SET Password= @newPassword";
        //            SqlCommand query = new SqlCommand(querystr, connection);
        //            query.Parameters.AddWithValue("@newPassword", _newPassword);
        //            query.ExecuteNonQuery();
        //            command.Connection.Close();
                    

        //            return true;
        //        }
        //        else
        //        {
        //            reader.Dispose();
        //            cmd.Dispose();
        //            return false;
        //        }
                
        //    }
        //}
        

    
