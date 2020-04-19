using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;


namespace Alexander.Models
{
    public class Beer
    {
        protected string beerType;
        protected int keg20_amount;
        protected int keg30_amount;
        protected int bottleCase_amount;
        protected float waste_AVG;

        
        public Beer(string beerType, int keg20_amount, int keg30_amount, int bottleCase_amount, float waste_AVG)
        {
            this.BeerType = beerType;
            this.Keg20_amount = keg20_amount;
            this.Keg30_amount = keg30_amount;
            this.BottleCase_amount = bottleCase_amount;
            this.Waste_AVG = waste_AVG;
        }

        public Beer()
        {
        }

        public string BeerType { get => beerType; set => beerType = value; }
        public int Keg20_amount { get => keg20_amount; set => keg20_amount = value; }
        public int Keg30_amount { get => keg30_amount; set => keg30_amount = value; }
        public int BottleCase_amount { get => bottleCase_amount; set => bottleCase_amount = value; }
        public float Waste_AVG { get => waste_AVG; set => waste_AVG = value; }


        public List<Beer> get_Beers()
        {
            DBservices dbs = new DBservices();

            List<Beer> beer_arr = dbs.get_BeersDB();

            return beer_arr;
        }

        public List<int> get_Tanks()
        {
            DBservices dbs = new DBservices();

            List<int> tanks_lst = dbs.get_TanksDB();

            return tanks_lst;
        }

        
    }
}