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
            int numEffected = 0;

            try // create new row in BatchAfterProd_2020 with the same id
            {
                numEffected = dbs.insert(this); // insert to Batch_2020

                dbs = dbs.read("[BatchAfterProd_2020]");
                dbs.dt.Rows.Add(BatchID, 0, 0, 0, 0, 0); 
                numEffected += dbs.update();

                dbs = dbs.read("[BatchAtProd_2020]");
                dbs.dt.Rows.Add(BatchID, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                numEffected += dbs.update();
            }
            catch (InvalidOperationException ex) // not found
            {
                string message = string.Format("Unable to add batch_id: {0} in table", batchID);
                throw new InvalidOperationException(message, ex);
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

            try
            {
                dbs.dt = edit(dbs.dt, this.batchID); // edit date & beer type
                effected += dbs.update(); // update DB
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try // edit brew tbl
            {
                dbs = dbs.read("[Brew_2020]");
                DataRow dr = dbs.dt.Select("batch_id=" + batchID).First();

                string st = "";

                foreach (Product prd in recipe_for_this_batch.Products_in_recipe) 
                {
                    /// Create prod string
                    if (recipe_for_this_batch.Products_in_recipe.IndexOf(prd) == recipe_for_this_batch.Products_in_recipe.Count - 1) // Last item in iteration
                    {
                        st += prd.ProductName + ":" + prd.Amount;
                        break;
                    }
                    st += prd.ProductName + ":" + prd.Amount + ",";
                }

                dr["prodItems"] = st;
                effected += dbs.update(); // update DB

                dbs = dbs.read("[Brew_2020]"); // read Again After Edit

                foreach (Product prd in recipe_for_this_batch.Products_in_recipe)
                {
                    float am = 0;
                    try /// Update amounts in Batch_2020
                    {
                        am = prd.Calc_inventory_amounts(prd.ProductName);
                        am -= prd.Calc_Brew_Amounts(prd.ProductName);
                        prd.Update_Product_Total_Amount(am, prd.ProductName);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                }
            catch (Exception ex)
            {
                throw ex;
            }
            

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
        public int delete_line(int id)
        {
            DBservices dbs = new DBservices();

            try
            {
                dbs = dbs.read("[BatchAfterProd_2020]");
                dbs.dt.Select("batch_id=" + id).First().Delete(); // Delete a line in DataTable
                dbs.update(); // update the DB

                dbs = dbs.read("[BatchAtProd_2020]");
                dbs.dt.Select("batch_id=" + id).First().Delete(); 
                dbs.update(); // update the DB

                dbs = dbs.read("[Brew_2020]");
                dbs.dt.Select("batch_id=" + id).First().Delete(); 
                dbs.update(); // update the DB

                dbs = dbs.read("[Batch_2020]");
                dbs.dt.Select("batch_id=" + id).First().Delete(); 
                dbs.update(); // update the DB

            }
            catch (InvalidOperationException ex) // Delete cannot exe
            {
                string message = string.Format("Unable to find batchID: {0} in table", id);
                throw new InvalidOperationException(message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return 1;
        }

        public double AverageWastePercetage(string beer_name) // CHECK THIS
        {
            DBservices dbs = new DBservices();
            dbs = dbs.SendSQLQuery($@"SELECT AVG(waste_precent) AS WasteAVG
                                     FROM [BatchAfterProd_2020]  right JOIN  [Batch_2020] ON [BatchAfterProd_2020].batch_id=[Batch_2020].batch_id
                                     WHERE [beer_type]='{beer_name}'");
            
            return (double)dbs.dt.Rows[0]["WasteAVG"];
        }
    }
}