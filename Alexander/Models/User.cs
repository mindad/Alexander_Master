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

        //update new passowrd
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

                    }// cahnge password  manager
                    else
                        dr[0]["password"] = NewPass;

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
        public string get_oldpass(string send_address) // 
        {

            DBservices dbs = new DBservices();
            dbs = dbs.read("[User_2020]");

            try
            {
                DataRow[] dr = dbs.dt.Select("userName='" + username + "' AND question1='" + question + "'"); // 
                if (dr.Length != 0)
                {
                    if ((string)dr[0]["userName"] == "sahar")
                    {
                        return (string)dr[0]["password"];
                    }
                    return (string)dr[0]["password"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "";
        

        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("alexanderbeers5@gmail.com", "amit$bg305");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("alexanderbeers5@gmail.com", "Alexander pass Manager");
            mail.To.Add(new MailAddress(send_address));
            mail.Subject = "";
            mail.Body = "<h1>Yor Password Is </h1><p>Insert pass here</p>";
            mail.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mail);
                return "Success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}