using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;


namespace Alexander.Controllers
{
    public class BeerController : ApiController
    {

        public List<Beer> Get()
        {
            Beer beer = new Beer();
            return beer.get_Beers();
        }

        [HttpGet]
        [Route("api/Beer/Tanks")]
        public List<int> GetTanks()
        {
            Beer beer = new Beer();
            return beer.get_Tanks();
        }

        public int Post([FromBody]Beer beer)
        {
            return beer.insert();
        }
    }
}
