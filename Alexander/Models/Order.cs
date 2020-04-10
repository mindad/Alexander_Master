using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Order
    {
        private int orderID;
        private DateTime date;
        private Beer beer;

        public Order() { }

        public Order(int orderID, DateTime date, Beer beer)
        {
            this.orderID = orderID;
            this.date = date;
            this.beer = beer;
        }

        public int OrderID { get => orderID; set => orderID = value; }
        public DateTime Date { get => date; set => date = value; }
        public Beer Beer { get => beer; set => beer = value; }




        public List<Order> get_Orders()
        {
            DBservices dbs = new DBservices();

            List<Order> Order_arr = dbs.get_OrdersDB();

            return Order_arr;
        }

        // getters + setters


    }
}