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
        private DBNull outputParam;

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

            cmd.CommandTimeout = 20;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }


        //GET ALL MANAGER PRODUCT
        
                  public List<managerproducts> get_manager_productsDB()
        {
            List<managerproducts> manager_products_List = new List<managerproducts>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [manager_products_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   // Read till the end of the data into a row

                    managerproducts mp = new managerproducts();
                    mp.ProdName=(string)dr["prodName"];
                    mp.ProdID = Convert.ToInt32(dr["prodID"]);
                    mp.BeerType = (string)dr["beerType"];
                 mp.Amount = Convert.ToInt32(dr["amount"]);
                    mp.Min_in_stock = Convert.ToInt32(dr["min_In_Stock"]);
                 

                    manager_products_List.Add(mp);
                }

                return manager_products_List;
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

        //END GET ALL MANAGER PRODUCTS
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
        /// INSERT all Batches
        /// 
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

    
    //Get All Orders   
        public List<Order> get_OrdersDB()
        {
            List<Order> Order_List = new List<Order>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [Order_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Order order = new Order();

                    //TODO add the beer type
                    order.OrderID = Convert.ToInt32(dr["Order_id"]);
                    order.Date = Convert.ToDateTime(dr["SupplyDate"]);


                    order.Keg20_inOrder= (Convert.ToInt32(dr["keg_20_amount"]));
                    order.Keg30_inOrder= (Convert.ToInt32(dr["keg_30_amount"]));
                    order.Box24_inOrder= (Convert.ToInt32(dr["box_24"]));
                    order.BeerType= (string)(dr["beerType"]);
                
            
         
                    Order_List.Add(order);
                }
                return Order_List;
              
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

        //end orders

        //Get All Users   
        public List<User> get_usersDB()
        {
            List<User> User_List = new List<User>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [User_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   // Read till the end of the data into a row

                    User U = new User();
                    
                    U.Username= (string)(dr["userName"]);
                    U.Password= (string)(dr["password"]);
                    U.Email= (string)(dr["email"]);
                    U.Question= (string)(dr["question1"]);
                    U.Site= (string)(dr["website"]);

                    User_List.Add(U);
                }
                return User_List;

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

        //end Users

        /// INSERT all BatcheBatchebotteling
        /// 
        public int insert(Batch_Botteling Batch_Botteling)
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
                String cStr = BuildInsertCommand(Batch_Botteling);      // helper method to build the insert string
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

        private String BuildInsertCommand(Batch_Botteling Batch_Botteling)
        {
            String command;

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Values({0}, {1}, {2}, {3}, '{4}', {5}, '{6}' ,{7}, '{8}')", Batch_Botteling.BatchID,
                Batch_Botteling.Keg20_amount, Batch_Botteling.Keg30_amount,
                Batch_Botteling.Bottels_qty, Batch_Botteling.Waste_litter,
                Batch_Botteling.Wort_volume, Batch_Botteling.Date.ToString("yyyy-MM-dd"),
                Batch_Botteling.BeerType, Batch_Botteling.Tank);
            String prefix = "INSERT INTO BatchAfterProd_2020 " + "([batch_id], [keg_20_amount], [keg_30_amount], [bottles_qty], [waste_litter], [wort_volume], [tank], [beer_type] ";
            command = prefix + sb.ToString();

            return command;
        }

        // Batchebotteling END


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
                    string[] parsed_items_in_recipe = ((string)dr["prods_in_recipe"]).Split(',');
                    List<Product> prd_lst = new List<Product>();

                    for (int i = 0; i < parsed_items_in_recipe.Length; i++)
                    {
                        Product prod = new Product();

                        prod.ProductType = parsed_items_in_recipe[i].Split(':')[0];
                        //prod.Amount = (float)Convert.ToDouble(parsed_items_in_recipe[i].Split(':')[1]);
                        prod.Min_amount = 0;
                        //prod.Last_arrivalTime = new DateTime(); // not needed

                        //TODO make sure what ever happns here works

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
        /// GET all get_Batch_BottelingDB
        /// 
        public List<Batch_Botteling> get_Batch_BottelingDB()
        {
            List<Batch_Botteling> Batch_Botteling_List = new List<Batch_Botteling>();
            SqlConnection con = null;
            //select * from [dbo].[BatchAfterProd_2020] INNER JOIN [dbo].[Batch_2020] ON [BatchAfterProd_2020].batch_id=[Batch_2020].batch_id
            try
            {
                con = connect("DBConnectionString");
                // connect 2 table to 1 table with ID key
                String query = "select  [Batch_2020].[batch_id],[Batch_2020].[date],[Batch_2020].[tank],[Batch_2020].[wort_volume],[Batch_2020].[beer_type],[dbo].[BatchAfterProd_2020].[keg_20_amount] , [dbo].[BatchAfterProd_2020].[keg_30_amount] , [dbo].[BatchAfterProd_2020].[bottles_qty] , [dbo].[BatchAfterProd_2020].[waste_litter] ,[dbo].[BatchAfterProd_2020].[purge_amount] , [dbo].[BatchAfterProd_2020].[prod_waste] ,[dbo].[BatchAfterProd_2020].[harvest_amount] ,[dbo].[BatchAfterProd_2020].[beer_req_litter] ,[dbo].[BatchAfterProd_2020].[filling_hose] ,[dbo].[BatchAfterProd_2020].[tank_leftover]   from [dbo].[BatchAfterProd_2020]  right JOIN  [dbo].[Batch_2020] ON [BatchAfterProd_2020].batch_id=[Batch_2020].batch_id";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {   // Read till the end of the data into a row

                    Batch_Botteling Batch_Botteling = new Batch_Botteling();

                    //check if nul from SQL
                    Batch_Botteling.BatchID = Convert.ToInt32(dr["batch_id"]);
                    if (dr["keg_20_amount"] != DBNull.Value)
                    {
                        Batch_Botteling.Keg20_amount = Convert.ToInt32(dr["keg_20_amount"]);
                    }
                    if (dr["keg_30_amount"] != DBNull.Value)
                    {
                        Batch_Botteling.Keg30_amount = Convert.ToInt32(dr["keg_30_amount"]);
                    }
                    if (dr["bottles_qty"] != DBNull.Value)
                    {
                        Batch_Botteling.Bottels_qty = Convert.ToInt32(dr["bottles_qty"]);
                    }
                    if (dr["waste_litter"] != DBNull.Value)
                    {
                        Batch_Botteling.Waste_litter = Convert.ToInt32(dr["waste_litter"]);
                    }

                    //Batch_Botteling.Waste_percent = (float)dr["waste_precent"];
                    Batch_Botteling.Date = Convert.ToDateTime(dr["date"]);
                    Batch_Botteling.Tank = Convert.ToInt32(dr["tank"]);
                    Batch_Botteling.Wort_volume = (float)dr["wort_volume"];
                    Batch_Botteling.BeerType = (string)dr["beer_type"];



                    Batch_Botteling_List.Add(Batch_Botteling);
                }

                return Batch_Botteling_List;
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
        /// GET all Products from Product_2020
        /// 
        public List<Product> get_ProductsDB()
        {
            List<Product> prod_list = new List<Product>();
            SqlConnection con = null;

            try
            {
                con = connect("DBConnectionString");

                String query = "SELECT * FROM [Product_2020]";
                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // the connection will close as reading completes

                while (dr.Read())
                {

                    Product prod = new Product();

                    prod.ProductName = (string)dr["prodName"];
                    prod.ProductType = (string)dr["prodType"];
                    prod.ProductID = Convert.ToInt32(dr["prodID"]);
                    prod.Min_amount = Convert.ToInt32(dr["minimum_amount"]);

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

        ///// 
        ///// GET all Products from inventory
        ///// 
        public List<Product> get_Inventory_ProductsDB()
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
        //public DBservices read_batches()
        //{
        //    SqlConnection con = null;
        //    try
        //    {
        //        con = connect("DBConnectionString");
        //        da = new SqlDataAdapter("select * from Batch_2020", con);
        //        SqlCommandBuilder builder = new SqlCommandBuilder(da);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds);
        //        dt = ds.Tables[0];
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return this;
        //}

        // read DT based on string parameter
        public DBservices read(string tbl_name)
        {
            SqlConnection con = null;
            try
            {
                con = connect("DBConnectionString");
                da = new SqlDataAdapter("select * from " + tbl_name, con);
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