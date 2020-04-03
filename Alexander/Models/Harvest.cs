using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Harvest
    {
        private int batchid;
        private DateTime date;
        private string name;
        private float temperature;
        private DateTime time_tap_2;
        private DateTime total_Duration;

        public Harvest(int batchid, DateTime date, string name, float temperature, DateTime time_tap_2, DateTime total_Duration)
        {
            this.batchid = batchid;
            this.date = date;
            this.name = name;
            this.temperature = temperature;
            this.time_tap_2 = time_tap_2;
            this.total_Duration = total_Duration;
        }

        public Harvest() { }

        public int Batchid { get => batchid; set => batchid = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Name { get => name; set => name = value; }
        public float Temperature { get => temperature; set => temperature = value; }
        public DateTime Time_tap_2 { get => time_tap_2; set => time_tap_2 = value; }
        public DateTime Total_Duration { get => total_Duration; set => total_Duration = value; }


        public List<Harvest> get_Harvest()
        {
            DBservices dbs = new DBservices();

            List<Harvest> harvest_arr = dbs.get_HarvestDB();

            return harvest_arr;
        }
    }
}