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
        //private string beerType;
        //private int keg20_inOrder;
        //private int keg30_inOrder;
        //private int box24_inOrder;


      

        //public string BeerType { get => beerType; set => beerType = value; }
        //public int Keg20_inOrder { get => keg20_inOrder; set => keg20_inOrder = value; }
        //public int Keg30_inOrder { get => keg30_inOrder; set => keg30_inOrder = value; }
        //public int Box24_inOrder { get => box24_inOrder; set => box24_inOrder = value; }

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


        //public Order(int orderID, DateTime date, string beerType, int keg20_inOrder, int keg30_inOrder, int box24_inOrder)
        //{
        //    OrderID = orderID;
        //    Date = date;
        //    BeerType = beerType;
        //    Keg20_inOrder = keg20_inOrder;
        //    Keg30_inOrder = keg30_inOrder;
        //    Box24_inOrder = box24_inOrder;
        //}

        public List<Order> get_Orders()
        {
            DBservices dbs = new DBservices();

            List<Order> Order_arr = dbs.get_OrdersDB();

            return Order_arr;
        }

        // getters + setters
      

    }
}