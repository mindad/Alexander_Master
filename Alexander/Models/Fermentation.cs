using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
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

        public int insert() // insert new row into Product_2020
        {
            int numEffected = 0;
            DBservices dbs = new DBservices();

            try
            {
                dbs = dbs.read("[Fermantation_2020]");

                dbs.dt.Rows.Add(-1, batchid, date, pressureChange, tank_pressure, tank_temperature, ferment);
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
            dbs = dbs.read("[Fermantation_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("[index]=" + row_num).First(); // ProductType Holds the original date to look in table

                dr["date"] = date;
                dr["pressure_change"] = pressureChange;
                dr["pressure_tank"] = tank_pressure;
                dr["tank_temperature"] = tank_temperature;
                dr["ferment"] = ferment;

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
            dbs = dbs.read("[Fermantation_2020]");

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