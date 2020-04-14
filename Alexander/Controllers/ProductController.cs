using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class ProductController : ApiController
    {

        public List<Product> Get()
        {
            Product prod = new Product();
            return prod.get_Products();
        }

        public int Post([FromBody]Product prod)
        {
            return prod.insert();
        }


        //[HttpPut]
        //[Route("api/Inventory/ChangeAmounts")]
        //public HttpResponseMessage Put_change_amount([FromBody]List<Product> prod_list) // 
        //{
        //    int numEffected = 0;
        //    Product prd = new Product();

        //    try
        //    {
        //        numEffected = prd.Decrease_Amounts(prod_list);

        //        if (numEffected > 0)
        //            return Request.CreateResponse(HttpStatusCode.OK, numEffected);
        //        else
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }

        //}


        [HttpDelete]
        public HttpResponseMessage Delete([FromBody]Product prod) // row = row number in DB
        {
            int numEffected = 0;
            //Product prod= new Product();

            try
            {
                numEffected = prod.delete_prod();

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
