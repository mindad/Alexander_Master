using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Text;

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

        public int insert(Batch batch)
        {
            SqlConnection con;
            SqlCommand cmd;
            int numEffected = 0;

            try
            {
                con = connect("DBConnectionString"); // create the connection
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            try
            {
                String cStr = BuildInsertCommand(batch);      // helper method to build the insert string
                cmd = CreateCommand(cStr, con);             // create the command
                numEffected += cmd.ExecuteNonQuery();       // Execute command
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
            return numEffected;
        }

        private String BuildInsertCommand(Batch batch)
        {
            String command;

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values({0}, '{1}', {2}, {3}, '{4}')", batch.BatchID, batch.Date.ToString("yyyy-MM-dd"), batch.Tank, batch.Wort_volume, batch.BeerType);
            String prefix = "INSERT INTO Batch_2020 " + "([batch_id], [date], [tank], [wort_volume], [beer_type]) ";
            command = prefix + sb.ToString();

            return command;
        }

        // Batches END



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

        /// 
        /// GET all Products
        /// 
        public List<Product> get_ProductsDB()
        {
            List<Product> prod_list = new List<Product>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [Inventory_Product_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {

                    Product prod = new Product();

                    prod.ProductName = (string)dr["prodName"];
                    prod.ProductType = (string)dr["prodType"];
                    prod.ProductID = Convert.ToInt32(dr["prodID"]);
                    prod.Amount = Convert.ToInt32(dr["amount"]);
                    prod.Min_amount = Convert.ToInt32(dr["minimum_amount"]);
                    prod.Last_arrivalTime = Convert.ToDateTime(dr["last_supply_date"]);

                    prod_list.Add(prod);
                }

                return prod_list;
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
        /// GET all Alerts [Alert_2020]
        /// 
        public List<alert> get_AlertsDB()
        {
            List<alert> alert_list = new List<alert>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [Alert_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {

                    alert alert_to_add = new alert();

                    alert_to_add.AlertID = Convert.ToInt32(dr["Alert_id"]);
                    alert_to_add.Type = (string)dr["type"];
                    alert_to_add.Date = Convert.ToDateTime(dr["date"]);
                    alert_to_add.Description = (string)dr["description"];
                    alert_to_add.Notes = (string)dr["notes"];

                    alert_list.Add(alert_to_add);
                }

                return alert_list;
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
        /// Edit batch_2020 tbl
        /// 
        public DBservices read_batches()
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from Batch_2020", con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return this;
        }

        // Global update for any dt
        public int update() 
        {
            try
            {
                da.Update(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 1;
        }
    }
}