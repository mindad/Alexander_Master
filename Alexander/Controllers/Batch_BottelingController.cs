using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class Batch_BottelingController : ApiController
    {
        public List<Batch_Botteling> Get()
        {
            Batch_Botteling batch = new Batch_Botteling();
            return batch.get_Batch_Botteling();
        }

        /// <summary>
        ///  Annual Reports
        /// </summary>
        /// <returns></returns>
        //year batch botteling tank get  Annual Reports
        [HttpGet]
        [Route("api/Batch_Botteling/thisyeartank")]
        public List<Batch_Botteling> Getthistank(string year)
        {
            Batch_Botteling batchyear = new Batch_Botteling();
            return batchyear.get_Batch_Bottelingyeartank(year);
        }

        //year batch bottleing   Annual Reports
        [HttpGet]
        [Route("api/Batch_Botteling/thisyear")]
        public List<Batch_Botteling> Getthis(string year)
        {
            Batch_Botteling batchyear = new Batch_Botteling();
            return batchyear.get_Batch_Bottelingyear(year);
        }

        /// <summary>
        /// End  Annual Reports
        /// </summary>
        /// <returns></returns>


        //post
        public int Post([FromBody]Batch_Botteling batchbotteling)
        {
            return batchbotteling.insert_Batch_Botteling_arr();
        }


        [HttpPut] // edit 
        public HttpResponseMessage Put(Batch_Botteling batch_bt)
        {
            int numEffected = 0;

            try
            {
                numEffected = batch_bt.Update();

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



        //delete
        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]string batch_id) // row = row number in DB
        {
            int numEffected = 0;
            Batch_Botteling Batch_Botteling = new Batch_Botteling();

            try
            {
                numEffected = Batch_Botteling.delete_line(Convert.ToInt32(batch_id));

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