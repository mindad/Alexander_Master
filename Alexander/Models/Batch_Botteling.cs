using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Batch_Botteling : Batch
    {
        public int keg20_amount;
        public int keg30_amount;
        public int total_production;
        public int bottels_qty;
        public int waste_litter;
        public int waste_percent;
        //public Waste waste;

        public Batch_Botteling()
        {
        }
    }
}