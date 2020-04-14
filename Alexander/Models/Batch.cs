using System;
using System.Collections.Generic;
using System.Data;
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

        public Batch(int batchID, DateTime date, int tank, float wort_volume, string beerType, Recipe recipe_for_this_batch)
        {
            BatchID = batchID;
            Date = date;
            Tank = tank;
            Wort_volume = wort_volume;
            BeerType = beerType;
            Recipe_for_this_batch = recipe_for_this_batch;
        }

        public List<Batch> get_Batches()
        {
            DBservices dbs = new DBservices();

            List<Batch> Batch_arr = dbs.get_BatchesDB();

            return Batch_arr;
        }

        public int insert()
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insert(this); // insert to Batch_2020

            try // create new row in BatchAfterProd_2020 with the same id
            {
                dbs = dbs.read("[BatchAfterProd_2020]");

                dbs.dt.Rows.Add(BatchID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); 
                numEffected += dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try // create new row in BatchAtProd_2020 with the same id
            {
                dbs = dbs.read("[BatchAtProd_2020]");

                dbs.dt.Rows.Add(BatchID, 0, 0, 0, 0, 0, 0, 0, 0, 0); 
                numEffected += dbs.update();
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
            dbs = dbs.read("[Batch_2020]");

            dbs.dt = edit(dbs.dt, this.batchID);

            effected = dbs.update(); // update DB

            return effected;
        }

        public DataTable edit(DataTable dt, int batchid)
        {
            DataRow dr = dt.Select("batch_id=" + batchid).First(); // gets the row where id == batchid

            dr["date"] = this.date;
            dr["beer_type"] = this.beerType;

            return dt;
        }

        // Delete All Batches
        public int delete_line(int row)
        {
            DBservices dbs = new DBservices();
            //DBservices dbsA = new DBservices();
            //DBservices dbsB = new DBservices();

            try
            {
                dbs = dbs.read("[BatchAfterProd_2020]");
                dbs.dt.Select("batch_id=" + row).First().Delete(); // Delete a line in DataTable
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                dbs = dbs.read("[BatchAtProd_2020]");
                dbs.dt.Select("batch_id=" + row).First().Delete(); // Delete a line in DataTable
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                dbs = dbs.read("[Brew_2020]");
                dbs.dt.Select("batch_id=" + row).First().Delete(); // Delete a line in DataTable
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                dbs = dbs.read("[Batch_2020]");
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