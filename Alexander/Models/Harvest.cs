using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
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
        private int row_num;

        public Harvest(int batchid, DateTime date, string name, float temperature, DateTime time_tap_2, DateTime total_Duration, int row_num)
        {
            this.batchid = batchid;
            this.date = date;
            this.name = name;
            this.temperature = temperature;
            this.time_tap_2 = time_tap_2;
            this.total_Duration = total_Duration;
            this.Row_num = row_num;
        }

        public Harvest() { }

        public int Batchid { get => batchid; set => batchid = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Name { get => name; set => name = value; }
        public float Temperature { get => temperature; set => temperature = value; }
        public DateTime Time_tap_2 { get => time_tap_2; set => time_tap_2 = value; }
        public DateTime Total_Duration { get => total_Duration; set => total_Duration = value; }
        public int Row_num { get => row_num; set => row_num = value; }

        public List<Harvest> get_Harvest()
        {
            DBservices dbs = new DBservices();

            List<Harvest> harvest_arr = dbs.get_HarvestDB();

            return harvest_arr;
        }


        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            dbs = dbs.read("[Harvest_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("[index]=" + row_num).First(); // ProductType Holds the original date to look in table

                dr["date"] = date;
                dr["name"] = name;
                dr["temperature"] = temperature;
                dr["timeForTapTwo"] = time_tap_2;
                dr["total_time"] = total_Duration;

                effected = dbs.update(); // update DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return effected;
        }

        public int delete_line(int row) //
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Harvest_2020]");

            try
            {
                dbs.dt.Select("[index]=" + row).First().Delete(); // 
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 1;
        }
    }
}