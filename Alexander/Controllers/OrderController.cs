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


    }
}