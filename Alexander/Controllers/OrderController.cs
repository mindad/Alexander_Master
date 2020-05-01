using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class OrderController : ApiController
    {
        // GET api/<controller>
        public List<Order> Get()
        {
            Order order = new Order();
            return order.get_Orders();
        }



        //post
        public HttpResponseMessage Post([FromBody]Order order)
        {
            int numEffected = 0;
            try
            {
                numEffected = order.CreateOrder();

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








        [HttpPut] // edit 
        public HttpResponseMessage Put(Order Order_or)
        {
            int numEffected = 0;

            try
            {
                numEffected = Order_or.Update();

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
        public HttpResponseMessage Delete([FromBody]Order or) // row = row number in DB
        {
            int numEffected = 0;
            //Product prod= new Product();

            try
            {
                numEffected = or.delete_or();

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