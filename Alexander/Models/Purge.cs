﻿using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Purge
    {
        private int batchid;
        private DateTime date;
        private string name;
        private float temperature;
        private float weight;
        // make sure no needed other type of units
        private float num_of_buckets;
        private string notes;
        private int row_num;

        public Purge(DateTime date, string name, float temperature, float weight, float num_of_buckets, string notes, int batchid, int row_num)
        {
            this.date = date;
            this.name = name;
            this.temperature = temperature;
            this.weight = weight;
            this.num_of_buckets = num_of_buckets;
            this.notes = notes;
            this.batchid = batchid;
            this.Row_num = row_num;
        }

        public Purge() { }

        public DateTime Date { get => date; set => date = value; }
        public string Name { get => name; set => name = value; }
        public float Temperature { get => temperature; set => temperature = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Num_of_buckets { get => num_of_buckets; set => num_of_buckets = value; }
        public string Notes { get => notes; set => notes = value; }
        public int Batchid { get => batchid; set => batchid = value; }
        public int Row_num { get => row_num; set => row_num = value; }

        public List<Purge> get_Purge()
        {
            DBservices dbs = new DBservices();

            List<Purge> purge_arr = dbs.get_PurgeDB();

            return purge_arr;
        }

        public int insert() // insert new row into Product_2020
        {
            int numEffected = 0;
            DBservices dbs = new DBservices();

            try
            {
                dbs = dbs.read("[Purge_2020]");

                dbs.dt.Rows.Add(-1, batchid, date, name, temperature, weight, num_of_buckets, notes);
                numEffected = dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return numEffected;
        }


        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Purge_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("[index]=" + row_num).First(); // ProductType Holds the original date to look in table

                dr["date"] = date;
                dr["name"] = name;
                dr["temperature"] = temperature;
                dr["weight"] = weight;
                dr["Num_Of_Buckets"] = num_of_buckets;
                dr["notes"] = notes;

                effected = dbs.update(); // update DB
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            return effected;
        }

        public int delete_line(int row) 
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Purge_2020]");

            try
            {
                dbs.dt.Select("[index]=" + row).First().Delete(); 
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