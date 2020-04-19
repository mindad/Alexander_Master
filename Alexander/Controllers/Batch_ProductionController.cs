using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class Batch_ProductionController : ApiController
    {

        public List<Batch_Production> Get()
        {
            Batch_Production batch = new Batch_Production();
            return batch.get_Batch();
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Batch_Production batch)
        {
            int numEffected = 0;

            try
            {
                numEffected = batch.Update();

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
