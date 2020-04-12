using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class HarvestController : ApiController
    {
        public List<Harvest> Get()
        {
            Harvest harvest = new Harvest();
            return harvest.get_Harvest();
        }

        public int Post([FromBody]Harvest harv)
        {
            return harv.insert();
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Harvest harvest)
        {
            int numEffected = 0;

            try
            {
                numEffected = harvest.Update();

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
            Harvest harv = new Harvest();

            try
            {
                numEffected = harv.delete_line(Convert.ToInt32(row_num));

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
