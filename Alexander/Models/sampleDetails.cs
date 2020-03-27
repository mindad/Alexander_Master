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
    }
}