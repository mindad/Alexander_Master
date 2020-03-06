using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

namespace Alexander.Models.DAL
{
    public class DBservices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public DBservices()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }



        /// 
        /// GET all Batches
        /// 
        public List<Batch> get_BatchesDB()
        {
            List<Batch> batch_List = new List<Batch>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [Batch_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Batch bt = new Batch();

                    bt.BatchID = Convert.ToInt32(dr["batch_id"]);
                    bt.Date = Convert.ToDateTime(dr["date"]);
                    bt.Tank = Convert.ToInt32(dr["tank"]);
                    bt.Wort_volume = (float)dr["wort_volume"];
                    bt.BeerType = (string)dr["beer_type"];

                    batch_List.Add(bt);
                }

                return batch_List;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        
        /// 
        /// GET all get_RecipesDB
        /// 
        public List<Recipe> get_RecipesDB()
        {
            List<Recipe> recipe_List = new List<Recipe>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [Recipe_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Recipe recipe = new Recipe();

                    recipe.BeerType = (string)dr["beerType"];
                    recipe.CreationDate = Convert.ToDateTime(dr["creationDate"]);

                    // parse here all prods in recipe
                    string [] parsed_items_in_recipe = ((string)dr["prods_in_recipe"]).Split(',');
                    List<Product> prd_lst = new List<Product>();

                    for (int i = 0; i < parsed_items_in_recipe.Length; i++) 
                    {
                        Product prod = new Product();

                        prod.ProductType = parsed_items_in_recipe[i].Split(':')[0];
                        prod.Amount = (float)Convert.ToDouble(parsed_items_in_recipe[i].Split(':')[1]);
                        prod.Min_amount = 0;
                        prod.Last_arrivalTime = new DateTime(); // not needed

                        prd_lst.Add(prod);
                        
                    }

                    recipe.Products_in_recipe = prd_lst;

                    recipe_List.Add(recipe); 
                }

                return recipe_List;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        
        /// 
        /// GET all Beers
        /// 
        public List<Beer> get_BeersDB()
        {
            List<Beer> beer_list = new List<Beer>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [BeerInStock_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   

                    Beer beer = new Beer();

                    beer.BeerType = (string)dr["beerType"];
                    beer.Keg20_amount = Convert.ToInt32(dr["keg_20_amount"]);
                    beer.Keg30_amount = Convert.ToInt32(dr["keg_30_amount"]);
                    beer.BottleCase_amount = Convert.ToInt32(dr["bottle_case_amount"]);

                    beer_list.Add(beer);
                }

                return beer_list;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


    }
}