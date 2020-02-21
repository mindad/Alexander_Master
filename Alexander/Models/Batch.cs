using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Batch
    {
        protected int batchID;
        protected DateTime date;
        protected int tank;
        protected float wort_volume;
        protected string beerType;

        public Batch() { }

        public Batch(int batchID, DateTime date, int tank, float wort_volume, string beerType)
        {
            this.batchID = batchID;
            this.date = date;
            this.tank = tank;
            this.wort_volume = wort_volume;
            this.beerType = beerType;
        }

        protected int BatchID { get => batchID; set => batchID = value; }
        protected DateTime Date { get => date; set => date = value; }
        protected int Tank { get => tank; set => tank = value; }
        protected float Wort_volume { get => wort_volume; set => wort_volume = value; }
        protected string BeerType { get => beerType; set => beerType = value; }
    }
}