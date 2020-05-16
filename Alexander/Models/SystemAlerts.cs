using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class SystemAlerts
    {
        private const string message_with_batch = "Warning: ";
        private const string message_min_amount = "Warning: Minimum Amount - ";
        
        public SystemAlerts() { }

    
        public void Check_For_Brewmiester_Alerts()
        {
            DBservices dbs = new DBservices();
            
            List<sampleDetails> smpl_list = dbs.get_Sample_Details_DB();
            dbs = dbs.read("[Batch_2020]");

            /// sampleDetails Alerts
            foreach (sampleDetails sample in smpl_list)
            {
                if (sample.Ph < 4 || sample.Ph > 5.5)
                {
                    Create_Brewmiester_Alert("PH value is: " + sample.Ph.ToString(), "PH", sample.Batch_id.ToString());
                }
                
                DataRow batch = dbs.dt.Select("batch_id=" + sample.Batch_id ).First();
                string beerType = (string)batch["beer_type"];

                if ( beerType != "Wheat" && (sample.Tank_temp < 18 || sample.Tank_temp > 21)) // beertype need to be different from wheat
                {
                    Create_Brewmiester_Alert("Tank Temperature value is: " + sample.Tank_temp.ToString(), "Tank Temperature", sample.Batch_id.ToString());
                }
                if ( beerType == "Wheat" && (sample.Tank_temp < 28 || sample.Tank_temp > 30))
                {
                    Create_Brewmiester_Alert("Tank Temperature value is: " + sample.Tank_temp.ToString(), "Tank Temperature", sample.Batch_id.ToString());
                }

               
            }
            /// Fermentation Alerts
            List<Fermentation> ferment_list = dbs.get_FermentDB();
            foreach (Fermentation fr in ferment_list)
            {
                if (fr.Tank_pressure < 0 || fr.Tank_pressure > 1.3)
                {
                    Create_Brewmiester_Alert(fr.Tank_pressure.ToString() , "Tank Pressure", fr.Batchid.ToString());
                }
            }
            /// Minimum Amount Alerts
            List <Product> prod_list = dbs.get_ProductsDB();
            foreach (Product item in prod_list)
            {
                if (item.Amount < item.Min_amount)
                {
                    Create_Brewmiester_Alert(item.Amount.ToString(), "Minimum Amount", item.ProductName);
                }
            }

        }

        public void Check_For_Waste_Alerts()
        {
            try
            {
                DBservices dbs = new DBservices();
                List<string> beerTypes = dbs.GetUniqueBeerTypes();
                List<Batch_Botteling> batch_Botteling_List = dbs.get_Batch_BottelingDB();
                batch_Botteling_List.Sort((p, q) => p.Date.CompareTo(q.Date)); // sort batch list by dates 

                foreach (var beer_name in beerTypes)
                {
                    double avg_waste = new Batch().AverageWastePercetage(beer_name); // get current beer waste AVG
                    int number_of_deviations = 0;
                    int num_of_iterations = 0;
                    foreach (Batch_Botteling batch in batch_Botteling_List)
                    {
                        if (batch.BeerType == beer_name)
                        {
                            num_of_iterations += 1;
                            
                            if (batch.Waste_percent > avg_waste + 3 || batch.Waste_percent < avg_waste - 3) // deviation of +-3% in % waste
                            {
                                number_of_deviations += 1;
                                if (number_of_deviations == 2) 
                                {
                                    Create_Brewmiester_Alert("Waste value: " + batch.Waste_percent.ToString(), "Waste", batch.BatchID.ToString()); // more then 2 deviations
                                    //Create_Manager_Alert() //// AMIT
                                }
                            }
                            if (num_of_iterations == 2) // out of last 2 batches for a certain beer type
                            {
                                break;
                            }
                        }   
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void Create_Brewmiester_Alert(string alert_value, string alertType, string batch_or_product)
        {
            string st = "";

            if (new string[] { "Tank Temperature", "PH", "Tank Pressure", "Waste" }.Contains(alertType))
            {
                st = message_with_batch + alert_value;
            }
            else if(alertType == "Minimum Amount")
            {
                st = message_min_amount + alert_value;
            }

            try
            {
                alert al = new alert();
                al.Date = DateTime.Now.Date;
                al.Description = st;
                al.Type = alertType;
                al.Batch_or_prod = batch_or_product;

                al.CreateAlert();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void Create_Manager_Alert(string alert_value, string alertType)
        {

        }

     
    }
}