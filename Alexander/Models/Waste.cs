using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Waste
    {
        public int purgeAmount;
        public int prodWaste;
        public int harvestAmount;
        public float beer_req_litter;
        public int filling_Hose;
        public int tank_Leftover;

        public Waste() { }
        public Waste(int purgeAmount, int prodWaste, int harvestAmount, float beer_req_litter, int filling_Hose, int tank_Leftover)
        {
            this.purgeAmount = purgeAmount;
            this.prodWaste = prodWaste;
            this.harvestAmount = harvestAmount;
            this.beer_req_litter = beer_req_litter;
            this.filling_Hose = filling_Hose;
            this.tank_Leftover = tank_Leftover;
        }

        // getters + setters
        public int PurgeAmount { get => purgeAmount; set => purgeAmount = value; }
        public int ProdWaste { get => prodWaste; set => prodWaste = value; }
        public int HarvestAmount { get => harvestAmount; set => harvestAmount = value; }
        public float Beer_req_litter { get => beer_req_litter; set => beer_req_litter = value; }
        public int Filling_Hose { get => filling_Hose; set => filling_Hose = value; }
        public int Tank_Leftover { get => tank_Leftover; set => tank_Leftover = value; }


        public List<Batch_Botteling> Get_Batches_with_same_tank_and_beer(string beer_name, int tank, DateTime date)
        {
            DBservices dbs = new DBservices();
            List<Batch_Botteling> batch_list = dbs.get_Batch_BottelingDB();
            List<Batch_Botteling> current_beer_list = new List<Batch_Botteling>();

            foreach (Batch_Botteling batch in batch_list)
            {
                if (batch.BeerType == beer_name && batch.Tank == tank && batch.Date != date)
                {
                    current_beer_list.Add(batch);
                }
            }

            return current_beer_list;
        }
    }
}