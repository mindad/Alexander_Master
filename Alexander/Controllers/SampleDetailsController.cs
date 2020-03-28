using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class SampleDetailsController : ApiController
    {
        public List<sampleDetails> Get()
        {
            sampleDetails sample = new sampleDetails();
            return sample.get_Samples();
        }
    }
}
