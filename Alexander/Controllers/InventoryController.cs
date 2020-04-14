using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class InventoryController : ApiController
    {
        public List<Product> Get()
        {
            Product prod = new Product();
            return prod.get_Inventory_Products();
        }

  
        public int Post([FromBody]Product prod)
        {
            return prod.insert_inventory();
        }


        [HttpPut] 
        public HttpResponseMessage Put([FromBody]Product prod) 
        {
            int numEffected = 0;

            try
            {
                numEffected = prod.Update();

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
        public HttpResponseMessage Delete([FromBody]Product prod) // row = row number in DB
        {
            int numEffected = 0;
            //Product prod= new Product();

            try
            {
                numEffected = prod.delete_line();

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
