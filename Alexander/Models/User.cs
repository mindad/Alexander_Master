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
        private string site;
        private string newPass;


        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public string NewPass { get => newPass; set => newPass = value; }
        public string Question { get => question; set => question = value; }
        public string Site { get => site; set => site = value; }

        public User()
        {
        }

        public User(string username, string password, string email, string newPass, string question, string site)
        {
            Username = username;
            Password = password;
            Email = email;
            NewPass = newPass;
            Question = question;
            Site = site;
        }

        public int insert()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[User_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("userName='" + username + "' AND password="+ password).First(); // 
                if (dr == null || dr.ItemArray.All(i => i is DBNull))
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 1;
        }




        //GET
        public List<User> get_users()
        {
            DBservices dbs = new DBservices();

            List<User> User_arr = dbs.get_usersDB();

            return User_arr;
        }



        //update


        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            dbs = dbs.read("[User_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("userName='" + Username  ).First();

                dr["password"] =Username ;///////////////
        
            }
            catch (Exception ex)
            {

                throw ex;
            }





            effected = dbs.update(); // update DB

            return effected;
        }




        //end update


        public string forgotPassword()
        {
            return null;
        }
    }
}