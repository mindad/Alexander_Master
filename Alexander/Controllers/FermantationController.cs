using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class FermantationController : ApiController
    {
        public List<Fermentation> Get()
        {
            Fermentation fermant = new Fermentation();
            return fermant.get_Fermant();
        }
    }
}
