using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class PurgeController : ApiController
    {
        public List<Purge> Get()
        {
            Purge prg = new Purge();
            return prg.get_Purge();
        }
    }
}
