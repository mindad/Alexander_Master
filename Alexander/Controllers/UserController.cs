using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class UserController : ApiController
    {
        // GET api/<controller>
      
        public List<User> Get()
        {
            User user = new User();
            return user.get_users();
        }
         public int Post([FromBody]User user)
        {
            return user.insert();
        }

 
    }
}