using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Waste
    {
        private DateTime start_period;
        private DateTime end_period;
        private double coefA;
        private double coefB;
        private double coefC;
        private double p_valueA;
        private double p_valueB;
        private double p_valueC;
        private string varA;
        private string varB;
        private string varC;

        public DateTime Start_period { get => start_period; set => start_period = value; }
        public DateTime End_period { get => end_period; set => end_period = value; }
        public string VarA { get => varA; set => varA = value; }
        public string VarB { get => varB; set => varB = value; }
        public string VarC { get => varC; set => varC = value; }
        public double CoefA { get => coefA; set => coefA = value; }
        public double CoefB { get => coefB; set => coefB = value; }
        public double CoefC { get => coefC; set => coefC = value; }
        public double P_valueA { get => p_valueA; set => p_valueA = value; }
        public double P_valueB { get => p_valueB; set => p_valueB = value; }
        public double P_valueC { get => p_valueC; set => p_valueC = value; }

        public Waste() { }

        public Waste(DateTime start_period, DateTime end_period, double coefA, double coefB, double coefC, double p_valueA, double p_valueB, double p_valueC, string varA, string varB, string varC)
        {
            this.start_period = start_period;
            this.end_period = end_period;
            this.coefA = coefA;
            this.coefB = coefB;
            this.coefC = coefC;
            this.p_valueA = p_valueA;
            this.p_valueB = p_valueB;
            this.p_valueC = p_valueC;
            this.varA = varA;
            this.varB = varB;
            this.varC = varC;
        }

        public List<Waste> get_Waste_Records(bool get_recent=false) 
        { 
            DBservices dbs = new DBservices();

            List<Waste> waste_arr = dbs.get_WasteDB(get_recent);

            return waste_arr;
        }



        public List<Batch_Botteling> Get_Batches_with_same_tank_and_beer(string beer_name, int tank, DateTime date)
        {
            DBservices dbs = new DBservices();
            List<Batch_Botteling> batch_list = dbs.get_Batch_BottelingDB();
            List<Batch_Botteling> current_beer_list = new List<Batch_Botteling>();

            foreach (Batch_Botteling batch in batch_list)
            {
                if (batch.BeerType == beer_name && batch.Tank == tank) // && batch.Date != date
                {
                    current_beer_list.Add(batch);
                }
            }

            return current_beer_list;
        }
    }
}