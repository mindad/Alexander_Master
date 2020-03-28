using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class sampleDetails
    {

        DateTime date;
        DateTime time;
        float tank_temp;
        float sample_temp;
        float rate;
        float gravity;
        float ph;
        string notes;

        public DateTime Date { get => date; set => date = value; }
        public DateTime Time { get => time; set => time = value; }
        public float Tank_temp { get => tank_temp; set => tank_temp = value; }
        public float Sample_temp { get => sample_temp; set => sample_temp = value; }
        public float Rate { get => rate; set => rate = value; }
        public float Gravity { get => gravity; set => gravity = value; }
        public float Ph { get => ph; set => ph = value; }
        public string Notes { get => notes; set => notes = value; }

        public List<sampleDetails> get_Samples() //int id
        {
            List<sampleDetails> smpl_list = new List<sampleDetails>();
            
            DBservices dbs = new DBservices();
            try
            {
                dbs = dbs.read("[SampleDetails_2020]");
                var tmp_list = dbs.dt.Select("batch_id=" ).ToList(); //+ id

                //foreach (var item in tmp_list)
                //{
                //    //sampleDetails x = new sampleDetails(Convert.ToInt32(item[""]);
                //}
            }
            catch (Exception)
            {

                throw;
            }
            

            //List<Batch> Batch_arr = dbs.get_SamplesDB();

            return smpl_list;
        }
    }
}