using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class Batch_ProductionController : ApiController
    {

        public List<Batch_Production> Get()
        {
            Batch_Production batch = new Batch_Production();
            return batch.get_Batch();
        }

    }
}
