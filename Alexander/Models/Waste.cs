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
        private double valA;
        private double valB;
        private double valC;
        private string varA;
        private string varB;
        private string varC;

        public DateTime Start_period { get => start_period; set => start_period = value; }
        public DateTime End_period { get => end_period; set => end_period = value; }
        public double ValA { get => valA; set => valA = value; }
        public double ValB { get => valB; set => valB = value; }
        public double ValC { get => valC; set => valC = value; }
        public string VarA { get => varA; set => varA = value; }
        public string VarB { get => varB; set => varB = value; }
        public string VarC { get => varC; set => varC = value; }

        public Waste() { }

        public Waste(DateTime start_period, DateTime end_period, double valA, double valB, double valC, string varA, string varB, string varC)
        {
            this.start_period = start_period;
            this.end_period = end_period;
            this.valA = valA;
            this.valB = valB;
            this.valC = valC;
            this.varA = varA;
            this.varB = varB;
            this.varC = varC;
        }


        public List<Waste> get_Waste_Records() 
        { 
            DBservices dbs = new DBservices();

            List<Waste> waste_arr = dbs.get_WasteDB();

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