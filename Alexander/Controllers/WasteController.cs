using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class WasteController : ApiController
    {
        public List<Waste> Get(bool get_recent)
        {
            Waste ws = new Waste();
            return ws.get_Waste_Records(get_recent);
        }

        [HttpGet]
        [Route("api/Waste/SimilarBatch")]
        public List<Batch_Botteling> Get_Similar_batches(string beer_name, int tank, DateTime date)
        {
            Waste ws = new Waste();
            return ws.Get_Batches_with_same_tank_and_beer(beer_name, tank, date);
        }
    }
}
