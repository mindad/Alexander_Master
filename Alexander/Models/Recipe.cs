using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;


namespace Alexander.Models
{
    public class Recipe
    {
        private string beerType;
        private DateTime creationDate;
        private Product[] products_in_recipe;

        public string BeerType { get => beerType; set => beerType = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public Product[] Products_in_recipe { get => products_in_recipe; set => products_in_recipe = value; }

        public int CreateRecipe() // need to implement
        {
            return 0;
        }

        public List<Recipe> get_Recipes()
        {
            DBservices dbs = new DBservices();

            List<Recipe> recipe_arr = dbs.get_RecipesDB();

            return recipe_arr;
        }
    }


    
}