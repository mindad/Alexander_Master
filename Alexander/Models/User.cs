using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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
                DataRow[] dr = dbs.dt.Select("userName='" + username + "' AND password='"+ password + "'"); // 
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




        //GET
        public List<User> get_users()
        {
            DBservices dbs = new DBservices();

            List<User> User_arr = dbs.get_usersDB();

            return User_arr;
        }
         //end get




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

            if (effected==1) // true
            {
                return "Password Changed Successfuly";
            }
            return "Password Didnt Change";
            
        }

        //End update new passowrd

        //forgot password

        

        public string get_oldpass()
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
        }

        //end forgot password


   
    }
}