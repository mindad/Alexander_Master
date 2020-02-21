using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        protected string BeerType { get => beerType; set => beerType = value; }
        protected int Keg20_amount { get => keg20_amount; set => keg20_amount = value; }
        protected int Keg30_amount { get => keg30_amount; set => keg30_amount = value; }
        protected int BottleCase_amount { get => bottleCase_amount; set => bottleCase_amount = value; }
        protected float Waste_AVG { get => waste_AVG; set => waste_AVG = value; }
    }
}