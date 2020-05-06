using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Order
    {
        private int orderID;
        private DateTime date;
        private Beer beer;
        private List<Beer> list_beer;

        public Order() { }

        public Order(int orderID, DateTime date, Beer beer,List<Beer> ls)
        {
            this.orderID = orderID;
            this.date = date;
            this.beer = beer;
            this.list_beer = ls;
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public DateTime Date { get => date; set => date = value; }
        public Beer Beer { get => beer; set => beer = value; }
        public List<Beer> List_beer { get => list_beer; set => list_beer = value; }


        //get

        public List<Order> get_Orders()
        {
            DBservices dbs = new DBservices();

            List<Order> Order_arr = dbs.get_OrdersDB();

            return Order_arr;
        }

        // getters + setters


        //get orders this year Annual Reports
        

    public List<Order> get_Orderthisyear()
        {
            DBservices dbs = new DBservices();

            List<Order> Order_arr = dbs.get_OrdersYearDB();

            return Order_arr;
        }

        //end get orders this year Annual Reports


        //



        //post


        public int CreateOrder() // need to implement
        {
            DBservices dbs = new DBservices();
            int numEffected = 0;


            try //
            {
                numEffected = dbs.insert(this); // insert to Order_2020

     
            }
  
            catch (Exception ex)
            {
                throw ex;
            }

            return numEffected;
        }



        //end post








        //edit
        //edit Order
        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[order_2020]");
            
            try
            {

                DataRow[] dr = dbs.dt.Select("Order_id='" + OrderID + "' AND beerType='" + Beer.BeerType + "'"); // 
                if (dr.Length != 0)
                {
                    dr[0]["keg_20_amount"] = Beer.Keg20_amount;
                    dr[0]["keg_30_amount"] = Beer.Keg30_amount;
                    dr[0]["box_24"] = Beer.BottleCase_amount;

                    effected = dbs.update(); // update DB
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }


            return effected;
        }

        //delete order row

        public int delete_or()
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[order_2020]");



            try // Delete from inventory
            {
                foreach (DataRow row in dbs.dt.Select("Order_id='" + OrderID + "' AND beerType='" + Beer.BeerType + "'"))
                {
                    row.Delete();
                }
                dbs.update();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return 1;
        }


        //

    }
}