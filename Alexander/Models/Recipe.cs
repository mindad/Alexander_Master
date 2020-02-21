using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Recipe
    {
        public string beerType;
        protected DateTime creationDate;

        public Recipe(string beerType, DateTime creationDate)
        {
            this.beerType = beerType;
            this.creationDate = creationDate;
        }

        public string BeerType { get => beerType; set => beerType = value; }
        protected DateTime CreationDate { get => creationDate; set => creationDate = value; }


        public int CreateRecipe() // need to implement
        {
            return 0;
        }
    }


    
}