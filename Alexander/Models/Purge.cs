using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Purge
    {
        DateTime date;
        string name;
        float temperature;
        float weight;
        // make sure no needed other type of units
        float num_of_buckets;
        string notes;

        public DateTime Date { get => date; set => date = value; }
        public string Name { get => name; set => name = value; }
        public float Temperature { get => temperature; set => temperature = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Num_of_buckets { get => num_of_buckets; set => num_of_buckets = value; }
        public string Notes { get => notes; set => notes = value; }
    }
}