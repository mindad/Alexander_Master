using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class PurgeController : ApiController
    {
        public List<Purge> Get()
        {
            Purge prg = new Purge();
            return prg.get_Purge();
        }

        [HttpPut] 
        public HttpResponseMessage Put([FromBody]Purge purge) 
        {
            int numEffected = 0;

            try
            {
                numEffected = purge.Update();

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

        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]string row_num) // row = row number in DB
        {
            int numEffected = 0;
            Purge prg = new Purge();

            try
            {
                numEffected = prg.delete_line(Convert.ToInt32(row_num));

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
