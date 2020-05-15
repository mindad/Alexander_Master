using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Alexander.Models
{
    public class User
    {
        private string username;
        private string password;
        private string email;
        private string question;
        private string newPass;


        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string NewPass { get => newPass; set => newPass = value; }
        public string Question { get => question; set => question = value; }


        public User()
        {
        }

        public User(string username, string password, string email, string newPass, string question)
        {
            Username = username;
            Password = password;
            Email = email;
            NewPass = newPass;
            Question = question;
        }

        public string Check_Password()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[User_2020]");

            try
            {
                DataRow[] dr = dbs.dt.Select("userName='" + username + "' AND password='" + password + "'"); // 
                if (dr.Length != 0)
                {
                    if ((string)dr[0]["userName"] == "sahar")
                    {
                        return "Brewmiester";
                    }
                    return "Manager";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "";
        }

        //update new passoword
        public string updatepass()
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            dbs = dbs.read("[User_2020]");

            try
            {

                DataRow[] dr = dbs.dt.Select("userName='" + username + "' AND password='" + password + "'"); // 
                if (dr.Length != 0)
                {
                    if ((string)dr[0]["userName"] == "sahar")
                    {
                        dr[0]["password"] = NewPass;

                    } // cahnge password  manager
                    else
                    {
                        dr[0]["password"] = NewPass;
                    }
                        
                    effected = dbs.update(); // update DB
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

            if (effected == 1) // true
            {
                return "Password Changed Successfuly";
            }
            return "Password Didnt Change";

        }

        //End update new passowrd

        //forgot password
        public bool get_oldpass() //// TODO: make default email in DB only
        {
            DBservices dbs = new DBservices();
            User admin = dbs.get_Single_UserDB("admin");
            dbs = dbs.read("[User_2020]");

            try
            {
                DataRow[] dr = dbs.dt.Select("userName='" + username + "'");
                if (dr.Length != 0)
                {
                    Password = (string)dr[0]["password"];
                    Email = (string)dr[0]["email"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (password != "")
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(admin.email, admin.password);
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(admin.email, "Alexander System");
                    mail.To.Add(new MailAddress(Email));
                    mail.Subject = "Your Pass To Alexander System";
                    mail.Body = $"<h1>Your Password Is: {password}</h1>";
                    mail.IsBodyHtml = true;

                    try
                    {
                        smtpClient.Send(mail);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return false;

        }

       
    }
}