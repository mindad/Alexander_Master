using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Fermentation
    {
        int batchid;
        DateTime date;
        float pressureChange;
        float tank_pressure;
        float tank_temperature;
        float ferment;

        public Fermentation(int batchid, DateTime date, float pressureChange, float tank_pressure, float tank_temperature, float ferment)
        {
            this.batchid = batchid;
            this.date = date;
            this.pressureChange = pressureChange;
            this.tank_pressure = tank_pressure;
            this.tank_temperature = tank_temperature;
            this.ferment = ferment;
        }

        public Fermentation() { }

        public int Batchid { get => batchid; set => batchid = value; }
        public DateTime Date { get => date; set => date = value; }
        public float PressureChange { get => pressureChange; set => pressureChange = value; }
        public float Tank_pressure { get => tank_pressure; set => tank_pressure = value; }
        public float Tank_temperature { get => tank_temperature; set => tank_temperature = value; }
        public float Ferment { get => ferment; set => ferment = value; }

        public List<Fermentation> get_Fermant()
        {
            DBservices dbs = new DBservices();

            List<Fermentation> fermant_arr = dbs.get_FermentDB();

            return fermant_arr;
        }
    }
}