using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class BatchController : ApiController
    {
        public List<Batch> Get()
        {
            Batch batch = new Batch();
            return batch.get_Batches();
        }

        public int Post([FromBody]Batch batch)
        {
            return batch.insert();
        }

        [HttpPut] // edit batch DATE OR BeerType
        public HttpResponseMessage Put([FromBody]Batch batch) // 
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


        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]string batch_id) // row = row number in DB
        {
            int numEffected = 0;
            Batch batch = new Batch();

            try
            {
                numEffected = batch.delete_line(Convert.ToInt32(batch_id));

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
