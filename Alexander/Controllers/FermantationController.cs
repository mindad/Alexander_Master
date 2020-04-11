using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class FermantationController : ApiController
    {
        public List<Fermentation> Get()
        {
            Fermentation fermant = new Fermentation();
            return fermant.get_Fermant();
        }

        [HttpPut] // 
        public HttpResponseMessage Put([FromBody]Fermentation ferm) // 
        {
            int numEffected = 0;

            try
            {
                numEffected = ferm.Update();

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
            Fermentation fer = new Fermentation();

            try
            {
                numEffected = fer.delete_line(Convert.ToInt32(row_num));

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
