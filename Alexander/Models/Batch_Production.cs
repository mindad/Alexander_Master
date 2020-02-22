using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Batch_Production : Batch
    {
        public float cast_volume;
        public int yeast_cycle;
        public float co2_vol;
        public DateTime pitchTime;
        public float og;
        public float fg;
        public float pitching_rate;
        public float tank_temp;
        public float set_temp;
        

        public Batch_Production() { }

        //public Batch_Production(int batchID, DateTime date, int tank, float wort_volume, string beerType, float cast_volume)
        //{
        //    this.cast_volume = cast_volume;
        //}




    }
}