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

        //post login user name
         public string Post([FromBody]User user)
        {
            return user.Check_Password();
        }
        //End post login user name

        // post forgot password
        [HttpPost]
        [Route("api/User/Forgotpass")]
        public string PostForgot([FromBody]User user)
        {
         
            return user.get_oldpass();
        }
        // End post forgot password




        //put change passowrd
        [HttpPut]
        [Route("api/User/Changepass")]
     
        public HttpResponseMessage Put([FromBody]User u) // 
        {

            try
            {
                string Effected = u.updatepass();

                if (Effected == "Password Changed Successfuly")
                    return Request.CreateResponse(HttpStatusCode.OK, Effected);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        //End put change passowrd

    }
}