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
        public Beer [] beers_in_order;

        public Order() { }


        // getters + setters
        public int OrderID { get => orderID; set => orderID = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}