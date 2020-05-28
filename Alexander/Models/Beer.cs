using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;
using System.Data;


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


        public int insert()
        {
            DBservices dbs = new DBservices();
            int numEffected = 0;

            try // create new row in BatchAfterProd_2020 with the same id
            {
                numEffected = dbs.insert(this);

                int max_ProdID = dbs.Get_Max_prod_id() + 1;

                if(max_ProdID > 1)
                {
                    dbs = dbs.read("[manager_products_2020]");
                    string[] init_manager_products = { "Box24", "LabelBack", "LabelFront", "LabelNeck" };

                    foreach (var prod in init_manager_products)
                    {
                        dbs.dt.Rows.Add(prod, max_ProdID, BeerType, 0, 0);
                    }

                    numEffected += dbs.update();
                }
                else
                {
                    throw new InvalidOperationException("Unable to Insert product values");
                }

            }
            catch (InvalidOperationException ex) // not found
            {
                string message = string.Format("Unable to Insert values for beer: {0}", BeerType);
                throw new InvalidOperationException(message, ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return numEffected;
        }

    }
}