using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
	


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


        // getters + setters
        public int PurgeAmount { get => purgeAmount; set => purgeAmount = value; }
        public int ProdWaste { get => prodWaste; set => prodWaste = value; }
        public int HarvestAmount { get => harvestAmount; set => harvestAmount = value; }
        public float Beer_req_litter { get => beer_req_litter; set => beer_req_litter = value; }
        public int Filling_Hose { get => filling_Hose; set => filling_Hose = value; }
        public int Tank_Leftover { get => tank_Leftover; set => tank_Leftover = value; }



    }
}