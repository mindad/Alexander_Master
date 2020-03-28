using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Batch_Production : Batch
    {
        private float cast_volume;
        private int yeast_cycle;
        private float co2_vol;
        private string pitchTime;
        private float og;
        private float fg;
        private float pitching_rate;
        private float tank_temp;
        private float set_temp;

        public float Cast_volume { get => cast_volume; set => cast_volume = value; }
        public int Yeast_cycle { get => yeast_cycle; set => yeast_cycle = value; }
        public float Co2_vol { get => co2_vol; set => co2_vol = value; }
        public string PitchTime { get => pitchTime; set => pitchTime = value; }
        public float Og { get => og; set => og = value; }
        public float Fg { get => fg; set => fg = value; }
        public float Pitching_rate { get => pitching_rate; set => pitching_rate = value; }
        public float Tank_temp { get => tank_temp; set => tank_temp = value; }
        public float Set_temp { get => set_temp; set => set_temp = value; }

        public Batch_Production() { }

        public Batch_Production(float cast_volume, int yeast_cycle, float co2_vol, string pitchTime, float og, float fg, float pitching_rate, float tank_temp, float set_temp)
        {
            this.cast_volume = cast_volume;
            this.yeast_cycle = yeast_cycle;
            this.co2_vol = co2_vol;
            this.pitchTime = pitchTime;
            this.og = og;
            this.fg = fg;
            this.pitching_rate = pitching_rate;
            this.tank_temp = tank_temp;
            this.set_temp = set_temp;
        }


        public List<Batch_Production> get_Batch()
        {
            DBservices dbs = new DBservices();

            List<Batch_Production> Batch_arr = dbs.get_Batch_AtProduction();

            return Batch_arr;
        }

        




    }
}