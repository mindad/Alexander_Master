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
        private int row_num;

        public Fermentation(int batchid, DateTime date, float pressureChange, float tank_pressure, float tank_temperature, float ferment, int row_num)
        {
            this.batchid = batchid;
            this.date = date;
            this.pressureChange = pressureChange;
            this.tank_pressure = tank_pressure;
            this.tank_temperature = tank_temperature;
            this.ferment = ferment;
            this.row_num = row_num;
        }

        public Fermentation() { }

        public int Batchid { get => batchid; set => batchid = value; }
        public DateTime Date { get => date; set => date = value; }
        public float PressureChange { get => pressureChange; set => pressureChange = value; }
        public float Tank_pressure { get => tank_pressure; set => tank_pressure = value; }
        public float Tank_temperature { get => tank_temperature; set => tank_temperature = value; }
        public float Ferment { get => ferment; set => ferment = value; }
        public int Row_num { get => row_num; set => row_num = value; }

        public List<Fermentation> get_Fermant()
        {
            DBservices dbs = new DBservices();

            List<Fermentation> fermant_arr = dbs.get_FermentDB();

            return fermant_arr;
        }
    }
}