using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Alexander.Models;

namespace Alexander.Controllers
{
    public class BatchController : ApiController
    {
        public List<Batch> Get()
        {
            Batch batch = new Batch();
            return batch.get_Batches();
        }
    }
}
