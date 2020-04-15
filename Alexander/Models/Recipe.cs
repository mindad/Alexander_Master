using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;
using System.Collections;
using System.Data;

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

        public List<Recipe> get_Recipes()
        {
            DBservices dbs = new DBservices();

            List<Recipe> recipe_arr = dbs.get_RecipesDB();

            return recipe_arr;
        }


        public int CreateRecipe() // need to implement
        {
            DBservices dbs = new DBservices();
            int numEffected = 0;

            string st = prod_String_for_DB(products_in_recipe);

            try // create new row in BatchAfterProd_2020 with the same id
            {
                dbs = dbs.read("[Recipe_2020]");

                dbs.dt.Rows.Add(beerType, creationDate, st);
                numEffected += dbs.update();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return numEffected;
        }

        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Recipe_2020]");
            
            string st = prod_String_for_DB(products_in_recipe);

            try
            {
                DataRow dr = dbs.dt.Select("[beerType]='" + BeerType + "'").First(); // ProductType Holds the original date to look in table

                dr["creationDate"] = CreationDate;
                dr["prods_in_recipe"] = st;
                

                effected = dbs.update(); // update DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return effected;
        }


        public int delete_line(string beer_type)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Recipe_2020]");
            int res = 0;

            try
            {
                dbs.dt.Select("[beerType]='" + beer_type + "'").First().Delete(); 
                res = dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return res;
        }


        public string prod_String_for_DB(List<Product> prd_list)
        {
            string st = "";
            foreach (Product prod in prd_list)
            {
                if (prd_list.IndexOf(prod) == prd_list.Count - 1) // Last item in iteration
                {
                    st += prod.ProductName + ":" + prod.Amount;
                    break;
                }
                st += prod.ProductName + ":" + prod.Amount + ",";
            }
            return st;
        }
    }
}