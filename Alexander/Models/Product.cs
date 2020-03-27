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



        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            dbs = dbs.read("[Inventory_Product_2020]");

            try
            {
                DataRow dr = dbs.dt.Select("prodName='" + ProductName + " AND last_supply_date='" + ProductType + "'").First();

                dr["amount"] = amount;
                dr["last_supply_date"] = Last_arrivalTime;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            

            

            effected = dbs.update(); // update DB

            return effected;
        }

    }
}