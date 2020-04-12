using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class sampleDetails
    {

        private DateTime date;
        private float tank_temp;
        private float sample_temp;
        private float rate;
        private float gravity;
        private float ph;
        private string notes;
        private int batch_id;
        private int row_num;

        public sampleDetails(DateTime date, float tank_temp, float sample_temp, float rate, float gravity, float ph, string notes, int batch_id, int row_num)
        {
            this.date = date;
            this.tank_temp = tank_temp;
            this.sample_temp = sample_temp;
            this.rate = rate;
            this.gravity = gravity;
            this.ph = ph;
            this.notes = notes;
            this.batch_id = batch_id;
            this.row_num = row_num;
        }

        public sampleDetails() { }

        public DateTime Date { get => date; set => date = value; }
        public float Tank_temp { get => tank_temp; set => tank_temp = value; }
        public float Sample_temp { get => sample_temp; set => sample_temp = value; }
        public float Rate { get => rate; set => rate = value; }
        public float Gravity { get => gravity; set => gravity = value; }
        public float Ph { get => ph; set => ph = value; }
        public string Notes { get => notes; set => notes = value; }
        public int Batch_id { get => batch_id; set => batch_id = value; }
        public int Row_num { get => row_num; set => row_num = value; }

        public List<sampleDetails> get_Samples() //int id
        {
            DBservices dbs = new DBservices();

            List<sampleDetails> smpl_arr = dbs.get_Sample_Details_DB();

            return smpl_arr;
        }

        public int insert() // insert new row into Product_2020
        {
            int numEffected = 0;
            DBservices dbs = new DBservices();

            try
            {
                dbs = dbs.read("[SampleDetails_2020]");

                dbs.dt.Rows.Add(-1, batch_id, date, tank_temp, sample_temp, rate, gravity, ph, notes); // -1 is for identity col
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

            dbs = dbs.read("[SampleDetails_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("[index]=" + row_num).First(); // ProductType Holds the original date to look in table

                dr["date"] = date;
                dr["Tank_temp"] = tank_temp;
                dr["Sample_Temp"] = sample_temp;
                dr["Rate"] = rate;
                dr["Gravity"] = gravity;
                dr["ph"] = ph;
                dr["notes"] = notes;

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
            dbs = dbs.read("[SampleDetails_2020]");

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