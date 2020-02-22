using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;

namespace Alexander.Models
{
    public class Batch
    {
        private int batchID;
        private DateTime date;
        private int tank;
        private float wort_volume;
        private string beerType;
        private Recipe recipe_for_this_batch;

        public int BatchID { get => batchID; set => batchID = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Tank { get => tank; set => tank = value; }
        public float Wort_volume { get => wort_volume; set => wort_volume = value; }
        public string BeerType { get => beerType; set => beerType = value; }
        public Recipe Recipe_for_this_batch { get => recipe_for_this_batch; set => recipe_for_this_batch = value; }

        public Batch() { }

        

        public List<Batch> get_Batches()
        {
            DBservices dbs = new DBservices();

            List<Batch> Batch_arr = dbs.get_BatchesDB();

            return Batch_arr;
        }
    }
}