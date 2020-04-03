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
    }
}
