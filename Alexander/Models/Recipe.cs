using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;
using System.Collections;

namespace Alexander.Models
{
    public class Recipe
    {
        private string beerType;
        private DateTime creationDate;
        private List<Product> products_in_recipe;

        public string BeerType { get => beerType; set => beerType = value; }
        public DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public List<Product> Products_in_recipe { get => products_in_recipe; set => products_in_recipe = value; }

        public int CreateRecipe() // need to implement
        {
            DBservices dbs = new DBservices();
            int numEffected = 0;

            try // create new row in BatchAfterProd_2020 with the same id
            {
                dbs = dbs.read("[Recipe_2020]");

                dbs.dt.Rows.Add(beerType, creationDate, products_in_recipe);
                numEffected += dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return numEffected;
        }

        public List<Recipe> get_Recipes()
        {
            DBservices dbs = new DBservices();

            List<Recipe> recipe_arr = dbs.get_RecipesDB();

            return recipe_arr;
        }

        public int delete_line(string beer_type)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Recipe_2020]");
            int res = 0;

            try
            {
                dbs.dt.Select("[beerType]=" + beer_type).First().Delete(); 
                res = dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }


    }
}