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
        private double waste_precent1;

        public int Keg20_amount { get => keg20_amount; set => keg20_amount = value; }
        public int Keg30_amount { get => keg30_amount; set => keg30_amount = value; }

        public int Bottels_qty { get => bottels_qty; set => bottels_qty = value; }
        public float Waste_litter { get => waste_litter; set => waste_litter = value; }
        public float Waste_percent { get => waste_percent; set => waste_percent = value; }
        public double Waste_precent1 { get => waste_precent1; set => waste_precent1 = value; }

        //public Waste waste;

        public Batch_Botteling() { }

        public Batch_Botteling(int batchID, DateTime date, int tank, float wort_volume, string beerType, Recipe recipe_for_this_batch, int keg20_amount, int keg30_amount, int bottels_qty, float waste_litter, float waste_percent, double waste_percent1) : base(batchID, date, tank, wort_volume, beerType, recipe_for_this_batch)
        {
            this.Keg20_amount = keg20_amount;
            this.Keg30_amount = keg30_amount;
            
            this.Bottels_qty = bottels_qty;
            this.Waste_litter = waste_litter;
            this.Waste_percent = waste_percent;
            this.Waste_precent1 = waste_precent1;
        }

        //  Annual Reports Get batches this year


        //  // Annual Reports Get batches this year by tank
        public List<Batch_Botteling> get_Batch_Bottelingyeartank(string year)
        {
            DBservices dbs = new DBservices();

            List<Batch_Botteling> Batch_Botteling_arr_year = dbs.get_Batch_BottelingyeartankDB(year);

            return Batch_Botteling_arr_year;
        }


        // Annual Reports Get batches this year by beertype
        public List<Batch_Botteling> get_Batch_Bottelingyear(string year)
        {
            DBservices dbs = new DBservices();

            List<Batch_Botteling> Batch_Botteling_arr_year = dbs.get_Batch_BottelingyearDB(year);

            return Batch_Botteling_arr_year;
        }
        // End Annual Reports Get batches this year

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

        //end insert

        // changed it to 'new' so it will override 'batch.Update()' function ////// TODO
        //edit batch_botteling
        new public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            try
            {

                //CATCH BEERTYPE
                dbs = dbs.SendSQLQuery("select [Batch_2020].[batch_id],[Batch_2020].[beer_type],[dbo].[BatchAfterProd_2020].[keg_20_amount] , [dbo].[BatchAfterProd_2020].[keg_30_amount] , [dbo].[BatchAfterProd_2020].[bottles_qty],[BatchAfterProd_2020].[waste_litter] From [dbo].[BatchAfterProd_2020]  right JOIN  [dbo].[Batch_2020] ON [BatchAfterProd_2020].batch_id=[Batch_2020].batch_id");
            
           
                DataRow dr = dbs.dt.Select("batch_id=" + BatchID).First(); // gets the row where id == batchid
                  string beertype = (string)dr["beer_type"];
                //END CATCH BEERTYPE
                //START EDIT
                //
                dbs = dbs.read("[BatchAfterProd_2020]");

                DataRow dr1 = dbs.dt.Select("batch_id='" + BatchID + "'").First();
                //
                int bottletocheck = Convert.ToInt32(dr1["bottles_qty"]);
                dr1["keg_20_amount"] = this.keg20_amount;
                dr1["keg_30_amount"] = this.keg30_amount;
                dr1["bottles_qty"] = this.bottels_qty;
                dr1["waste_litter"] = this.waste_litter;
                dr1["waste_precent"] = this.waste_percent;

                //END  EDIT

                effected = dbs.update();

                //call function sum keg20/keg30/bottle from specific beer in prod

                Beer beer = Calc_inventory_amount(beertype);

                //call function sum keg20/keg30/bottle from specific beer in orders
                Beer beerOrders = Calc_Order_amount(beertype);
             
                //call function update beerinstock_2020 
                Update_Beer_in_Stcok(beer, beerOrders, beertype);

                //call function update manager inventory 
                if(bottletocheck< this.bottels_qty)//only if the number of bottles increased!!!!
                Update_manager_inventory(beertype, this.bottels_qty- bottletocheck);

                //check if there is alert to post
                check_for_Alerts(beertype);

                // update DB
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return effected;
        }

        //post

        ////check if there is alert to post
        public void check_for_Alerts(string beertype)
        {
            try
            {

                DBservices dbs = new DBservices();
                dbs = dbs.read("[manager_products_2020]");

                DataRow dr2 = dbs.dt.Select("beerType='" + beertype + "' AND prodName='Box24'").First();

                if (Convert.ToInt32(dr2["amount"]) < Convert.ToInt32(dr2["min_In_Stock"]))//boxes
                {

                    dbs.insertManagerAlert(beertype, "Boxes Stock");

                }
                 


                DataRow[] data_rows = dbs.dt.Select("beerType='" + beertype + "' AND prodName LIKE 'Label%' ");//labels
                //

                foreach (var dr1 in data_rows)//foreach label in beertype
                {
                    if (Convert.ToInt32(dr1["amount"]) < Convert.ToInt32(dr1["min_In_Stock"]))
                    { dbs.insertManagerAlert(beertype, "Label Stock");//if there is more then 1 label its stop check the labels
                        break;
                    }

                }
                dbs.update();
                //

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //function to update manager inventory
        public void Update_manager_inventory(string beertype, int bottles )
        {
            try
            {
                // update BeerInStock_2020 table 
                DBservices dbs = new DBservices();
                dbs = dbs.read("[manager_products_2020]");

                DataRow dr = dbs.dt.Select("beerType='" + beertype + "' AND prodName='Box24'").First();         
                dr["amount"] = Convert.ToInt32(dr["amount"])-(bottles/24);//box/24
                dbs.update();

        
                DataRow[] data_rows = dbs.dt.Select("beerType='" + beertype + "' AND prodName LIKE 'Label%' ");
                //

                foreach (var dr1 in data_rows)//foreach label in beertype
                {
                    dr1["amount"] = Convert.ToInt32(dr1["amount"]) - bottles;

                }
                dbs.update();
                //

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //End function to update manager inventory 


        //function calc inventory amount--->>>>>>>>>>>>>>>>>>>>>>>sum keg20/keg30/bottle from specific beer in BatchAfterProd_2020
        public Beer Calc_inventory_amount(string BeerName)
        {
            Beer beer = new Beer();

            DBservices dbs = new DBservices();
            //    
            try
            {

                //CATCH BEERTYPE
                dbs = dbs.SendSQLQuery("select distinct [Batch_2020].[batch_id],[Batch_2020].[beer_type],[dbo].[BatchAfterProd_2020].[keg_20_amount] , [dbo].[BatchAfterProd_2020].[keg_30_amount] , [dbo].[BatchAfterProd_2020].[bottles_qty],[BatchAfterProd_2020].[waste_litter] From [BatchAfterProd_2020] right JOIN  [Batch_2020] ON [BatchAfterProd_2020].batch_id=[Batch_2020].batch_id");

                DataRow[] data_rows = dbs.dt.Select("beer_type='" + BeerName + "'");
                //

                foreach (var dr in data_rows)
                {
                    beer.Keg20_amount += Convert.ToInt32(dr["keg_20_amount"]);
                    beer.Keg30_amount += Convert.ToInt32(dr["keg_30_amount"]);
                    beer.BottleCase_amount += Convert.ToInt32(dr["bottles_qty"]);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return beer;
      
        }


        //end function calc inventory

        //function sum keg20/keg30/bottle from specific beer in order_2020
        public Beer Calc_Order_amount(string BeerName)
        {
            Beer beerOrders = new Beer();

            DBservices dbs = new DBservices();
            // Get the current date.
            DateTime thisDay = DateTime.Today;
            dbs = dbs.read("[order_2020]");
        
            DataRow[] data_rows = dbs.dt.Select("beerType='" + BeerName + "' AND SupplyDate<'" + thisDay + "'"); // 
            foreach (var dr in data_rows)
            {
                beerOrders.Keg20_amount += Convert.ToInt32(dr["keg_20_amount"]);
                beerOrders.Keg30_amount += Convert.ToInt32(dr["keg_30_amount"]);
                beerOrders.BottleCase_amount += Convert.ToInt32(dr["box_24"]);

            }
            beerOrders.BottleCase_amount = beerOrders.BottleCase_amount * 24;// convert to bottles *24
            return beerOrders;
        }


        //end function calc inventory

        //update beerinstock

        public void Update_Beer_in_Stcok(Beer beerbotteling, Beer beerorder ,string beertype)
        {
            try
            {
                // update BeerInStock_2020 table 
                DBservices dbs = new DBservices();
                dbs = dbs.read("[BeerInStock_2020]");

                DataRow dr = dbs.dt.Select("beerType='" + beertype + "'").First();
                dr["keg_20_amount"] = beerbotteling.Keg20_amount-beerorder.Keg20_amount;
                dr["keg_30_amount"] = beerbotteling.Keg30_amount-beerorder.Keg30_amount;
                dr["bottle_case_amount"] = beerbotteling.BottleCase_amount-beerorder.BottleCase_amount;
                dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //end update




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