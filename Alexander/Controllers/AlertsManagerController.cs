using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class AlertsManagerController : ApiController
    {

        public List<AlertsManager> Get()
        {
            AlertsManager almanager = new AlertsManager();
            return almanager.get_AlertsManager();
        }

        [HttpPut] // edit batch DATE OR BeerType
        public HttpResponseMessage Put(object[] st) // 
        {
            int numEffected = 0;
            AlertsManager al = new AlertsManager();

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
        public HttpResponseMessage Delete([FromBody]string alert_id) // delete from tbl ased on alert_id
        {
            int numEffected = 0;
            AlertsManager al = new AlertsManager();

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