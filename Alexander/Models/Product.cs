using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;

namespace Alexander.Models
{
    public class Product
    {
        protected string productType;
        protected float amount;
        protected int min_amount;
        protected DateTime last_arrivalTime;
        protected string productName;
        protected int productID;

        public Product() { }

        public Product(string productType, float amount, int min_amount, DateTime last_arrivalTime, string productName, int productID)
        {
            ProductType = productType;
            Amount = amount;
            Min_amount = min_amount;
            Last_arrivalTime = last_arrivalTime;
            ProductName = productName;
            ProductID = productID;
        }

        public float Amount { get => amount; set => amount = value; }
        public int Min_amount { get => min_amount; set => min_amount = value; }
        public DateTime Last_arrivalTime { get => last_arrivalTime; set => last_arrivalTime = value; }
        public string ProductType { get => productType; set => productType = value; }
        public string ProductName { get => productName; set => productName = value; }
        public int ProductID { get => productID; set => productID = value; }


        public int insert() // insert new row into Product_2020
        {
            int numEffected = 0;
            DBservices dbs = new DBservices();

            try
            {
                dbs = dbs.read("[Product_2020]");

                dbs.dt.Rows.Add(ProductName, ProductType, ProductID, Min_amount);
                numEffected = dbs.update(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return numEffected;
        }



        public int insert_inventory() // insert new row into Inventory_Product_2020
        {
            int numEffected = 0;
            DBservices dbs = new DBservices();

            try
            {
                dbs = dbs.read("[Inventory_Product_2020]");

                dbs.dt.Rows.Add(null, ProductName, ProductType, ProductID, Amount, Last_arrivalTime);
                numEffected = dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            float am = Calc_inventory_amounts();
            am -= Calc_Brew_Amounts(ProductName);
            Update_Product_Total_Amount(am);

            return numEffected;
        }

        public float Calc_inventory_amounts()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Inventory_Product_2020]");

            DataRow[] data_rows = dbs.dt.Select("prodName='" + ProductName + "'");

            float res = 0;

            foreach (var dr in data_rows)
            {
                res += Convert.ToInt32(dr["amount"]);
            }

            return res;
        }


        private float Calc_Brew_Amounts(string prod_name)
        {
            float res = 0;
            DBservices dbs = new DBservices();

            List<Brew> brewList = dbs.get_BrewDB();

            foreach (var brew in brewList)
            {
                foreach (var prd in brew.Prod_list)
                {
                    if (prod_name == prd.ProductName)
                    {
                        res += prd.Amount;
                    }
                }
            }

            return res;
        }

        private void Update_Product_Total_Amount(float am)
        {
            try
            {
                // Increase Product Amount 
                DBservices dbs = new DBservices();
                dbs = dbs.read("[Product_2020]");

                DataRow dr = dbs.dt.Select("prodName='" + ProductName + "'").First();
                dr["amount"] = am;
                dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // returns products with sum amount + latest inventory date
        public List<Product> get_Products() 
        {
            DBservices dbs = new DBservices();
            DBservices dbs_second = new DBservices();
            
            List<Product> prod_list = dbs.get_ProductsDB();

            dbs_second = dbs_second.read("[Inventory_Product_2020]");

            try
            {
                foreach (var prod in prod_list)
                {
                    float am = 0;
                    DateTime dt = new DateTime(2008, 3, 1);

                    foreach (DataRow item in dbs_second.dt.Rows) // 
                    {
                        if ((string)item["prodName"] == prod.productName)
                        {
                            DateTime last_arrival_time = Convert.ToDateTime(item["last_supply_date"]);

                            if (DateTime.Compare(dt, last_arrival_time) < 0) // dt is smaller than last_arrival
                            {
                                dt = last_arrival_time;
                            }

                            am += Convert.ToInt32(item["amount"]);
                        }
                    }

                    prod.Last_arrivalTime = dt;
                    prod.Amount = am;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return prod_list;
        }



        public List<Product> get_Inventory_Products()
        {
            DBservices dbs = new DBservices();

            List<Product> prod_list = dbs.get_Inventory_ProductsDB();

            return prod_list;
        }


        // delete line from Inventory_Product_2020
        public int delete_line() //int row
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Inventory_Product_2020]");

            try
            {
                dbs.dt.Select("prodName='" + ProductName.ToString() + "'" + " AND last_supply_date='" + Last_arrivalTime.ToString("yyyy-MM-dd") + "'").First().Delete(); // 
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 1;
        }

        // delete Product Completley from Product_2020 & Inventory_Product_2020
        public int delete_prod()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Inventory_Product_2020]");

            try // Delete from inventory
            {
                foreach (DataRow row in dbs.dt.Select("prodID=" + ProductID))
                {
                    row.Delete();
                }
                dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            dbs = dbs.read("[Product_2020]");

            try // Delete from product
            {
                dbs.dt.Select("prodID=" + ProductID).First().Delete(); // deletes product from product_2020
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 1;
        }



        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            dbs = dbs.read("[Inventory_Product_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("prodName='" + ProductName + "' AND last_supply_date='" + ProductType + "'").First(); // ProductType Holds the original date to look in table

                dr["amount"] = amount;
                dr["last_supply_date"] = Last_arrivalTime;
                effected = dbs.update(); // update DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            float am = Calc_inventory_amounts();
            am -= Calc_Brew_Amounts(ProductName);
            Update_Product_Total_Amount(am);

            return effected;
        }


        /// this is just a copy ==== add logic
        public int Decrease_Amounts(List<Product> prod_list)
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Product_2020]");


            foreach (var prod in prod_list)
            {
                try
                {
                    DataRow dr = dbs.dt.Select("prodName='" + prod.ProductName + "'").First();

                    Update_Product_Total_Amount(prod.amount);

                    effected += dbs.update(); // update DB
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            


            
            return effected;
        }

    }
}