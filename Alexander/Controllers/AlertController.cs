using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;


namespace Alexander.Controllers
{
    public class AlertController : ApiController
    {
        public List<alert> Get()
        {
            alert al = new alert();
            return al.get_Alerts();
        }

        [HttpPut] // edit batch DATE OR BeerType
        public HttpResponseMessage Put(object[] st) // 
        {
            int numEffected = 0;
            alert al = new alert();

            try
            {
                numEffected = al.Update(Convert.ToInt32(st[0]), (string)st[1]);

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
        public HttpResponseMessage Delete([FromBody]string alert_id) // row = row number in DB
        {
            int numEffected = 0;
            alert al = new alert();

            try
            {
                numEffected = al.delete_line(Convert.ToInt32(alert_id));

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
