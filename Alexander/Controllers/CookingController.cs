using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class CookingController : ApiController
    {

        public List<Brew> Get()
        {
            Brew brew = new Brew();
            return brew.get_Brews();
        }

        public int Post([FromBody]Brew brew)
        {
            return brew.insert();
        }
    }
}
