using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;

namespace Alexander.Models
{
    public class Batch_Botteling : Batch
    {
        private int keg20_amount;
        private int keg30_amount;

        private int bottels_qty;
        private float waste_litter;
        private float waste_percent;

        public int Keg20_amount { get => keg20_amount; set => keg20_amount = value; }
        public int Keg30_amount { get => keg30_amount; set => keg30_amount = value; }

        public int Bottels_qty { get => bottels_qty; set => bottels_qty = value; }
        public float Waste_litter { get => waste_litter; set => waste_litter = value; }
        public float Waste_percent { get => waste_percent; set => waste_percent = value; }

        //public Waste waste;

        public Batch_Botteling() { }

        public Batch_Botteling(int batchID, DateTime date, int tank, float wort_volume, string beerType, Recipe recipe_for_this_batch, int keg20_amount, int keg30_amount, int bottels_qty, float waste_litter, float waste_percent) : base(batchID, date, tank, wort_volume, beerType, recipe_for_this_batch)
        {
            this.Keg20_amount = keg20_amount;
            this.Keg30_amount = keg30_amount;

            this.Bottels_qty = bottels_qty;
            this.Waste_litter = waste_litter;
            this.Waste_percent = waste_percent;
        }


        public List<Batch_Botteling> get_Batch_Botteling()
        {
            DBservices dbs = new DBservices();

            List<Batch_Botteling> Batch_Botteling_arr = dbs.get_Batch_BottelingDB();

            return Batch_Botteling_arr;
        }

        //insert
        public int insert_Batch_Botteling_arr()
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insert(this);
            return numEffected;
        }

        // changed it to 'new' so it will override 'batch.Update()' function ////// TODO
        //edit batch_botteling
        new public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[BatchAfterProd_2020]");

            DataRow dr = dbs.dt.Select("batch_id=" + BatchID).First(); // gets the row where id == batchid

            dr["keg_20_amount"] = this.keg20_amount;
            dr["keg_30_amount"] = this.keg30_amount;
            dr["bottles_qty"] = this.bottels_qty;
            dr["waste_litter"] = this.waste_litter;

            effected = dbs.update(); // update DB

            return effected;
        }


        //delete batch botteling
        // changed it to 'new' so it will override 'batch.delete_line()' function ////// TODO
        new public int delete_line(int row)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[BatchAfterProd_2020]");

            try
            {
                dbs.dt.Select("batch_id=" + row).First().Delete(); // Delete a line in DataTable
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