using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class managerproducts
    {
        private string prodName;
        private int prodID;
        private string beerType;
        private int amount;
        private int min_in_stock;

        //get manager_products
        public List<managerproducts> get_managerproducts()
        {
            DBservices dbs = new DBservices();

            List<managerproducts> Manager_products_arr = dbs.get_manager_productsDB();

            return Manager_products_arr;
        }

        //edit manager_products
         public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[manager_products_2020]");
     
            try
            {

                DataRow[] dr = dbs.dt.Select("prodID='" + ProdID + "' AND prodName='" + ProdName + "'"); // 
                if (dr.Length != 0)
                {
                    dr[0]["amount"] = Amount;
                    dr[0]["min_In_Stock"] = Min_in_stock;

                    effected = dbs.update(); // update DB
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
       

            return effected;
        }



        public managerproducts(string prodName, int prodID, string beerType, int amount, int min_in_stock)
        {
            this.prodName = prodName;
            this.prodID = prodID;
            this.beerType = beerType;
            this.amount = amount;
            this.min_in_stock = min_in_stock;
        }
        public managerproducts()
        { }

        public string ProdName { get => prodName; set => prodName = value; }
        public int ProdID { get => prodID; set => prodID = value; }
        public string BeerType { get => beerType; set => beerType = value; }
        public int Amount { get => amount; set => amount = value; }
        public int Min_in_stock { get => min_in_stock; set => min_in_stock = value; }
    }
}