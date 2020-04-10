using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
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
    }
}