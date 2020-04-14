using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class managerproductsController : ApiController
    {
        // GET api/<controller>
        public List<managerproducts> Get()
        {
            managerproducts Managerproducts = new managerproducts();
            return Managerproducts.get_managerproducts();
        }



        [HttpPut] // edit 
        public HttpResponseMessage Put(managerproducts managerproducts_mn)
        {
            int numEffected = 0;

            try
            {
                numEffected = managerproducts_mn.Update();

                if (numEffected > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, numEffected);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


    }
}