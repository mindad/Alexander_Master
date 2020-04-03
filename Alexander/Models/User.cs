using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class User
    {
        private string username;
        private string password;
        private string email;
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
        //GET
        public List<User> get_users()
        {
            DBservices dbs = new DBservices();

            List<User> User_arr = dbs.get_usersDB();

            return User_arr;
        }

        public User()
        {
        }

        public string forgotPassword()
        {
            return null;
        }
    }
}