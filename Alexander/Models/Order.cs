using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Order
    {
        public int orderID;
        public DateTime date;
        private List<Beer> beers_in_order;

        public Order() { }

        public Order(int orderID, DateTime date, Beer beers_in_order)
        {
            OrderID = orderID;
            Date = date;
           
        }

        public List<Order> get_Orders()
        {
            DBservices dbs = new DBservices();

            List<Order> Order_arr = dbs.get_OrdersDB();

            return Order_arr;
        }

        // getters + setters
        public int OrderID { get => orderID; set => orderID = value; }
        public DateTime Date { get => date; set => date = value; }
        public List<Beer> Beers_in_order { get => beers_in_order; set => beers_in_order = value; }
    }
}