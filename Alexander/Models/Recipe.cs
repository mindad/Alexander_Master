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
        protected Product[] products_in_recipe;

        


        public int CreateRecipe() // need to implement
        {
            return 0;
        }
    }


    
}