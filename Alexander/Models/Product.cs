using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Product
    {
        protected string productType;
        protected float amount;
        protected int min_amount;
        protected DateTime last_arrivalTime;

        public Product() { }

        public Product(string productType, float amount, int min_amount, DateTime last_arrivalTime)
        {
            this.ProductType = productType;
            this.Amount = amount;
            this.Min_amount = min_amount;
            this.Last_arrivalTime = last_arrivalTime;
        }

        public float Amount { get => amount; set => amount = value; }
        public int Min_amount { get => min_amount; set => min_amount = value; }
        public DateTime Last_arrivalTime { get => last_arrivalTime; set => last_arrivalTime = value; }
        public string ProductType { get => productType; set => productType = value; }
    }
}