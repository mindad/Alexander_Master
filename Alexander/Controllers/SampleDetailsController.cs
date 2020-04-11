using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class SampleDetailsController : ApiController
    {
        public List<sampleDetails> Get()
        {
            sampleDetails sample = new sampleDetails();
            return sample.get_Samples();
        }


        [HttpPut] // 
        public HttpResponseMessage Put([FromBody]sampleDetails sample) // 
        {
            int numEffected = 0;

            try
            {
                numEffected = sample.Update();

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
            sampleDetails smp = new sampleDetails();

            try
            {
                numEffected = smp.delete_line(Convert.ToInt32(row_num));

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
