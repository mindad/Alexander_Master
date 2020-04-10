using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class managerproductsController : ApiController
    {
        // GET api/<controller>
        public List<managerproducts> Get()
        {
            managerproducts Managerproducts = new managerproducts();
            return Managerproducts.get_managerproducts();
        }

    }
}