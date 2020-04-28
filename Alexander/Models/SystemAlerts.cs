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
        private alert al;
        private const string message = "Warning: values are out of the range - ";

        public alert Al { get => al; set => al = value; }

        public SystemAlerts() { }

        public SystemAlerts(alert al)
        {
            Al = al;
        }

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
                    Create_Brewmiester_Alert(sample.Batch_id.ToString() , "PH " + sample.Ph.ToString());
                }
                
                DataRow batch = dbs.dt.Select("batch_id=" + sample.Batch_id ).First();
                string beerType = (string)batch["beer_type"];

                if ( beerType != "Wheat" && (sample.Tank_temp < 18 || sample.Tank_temp > 21)) // beertype need to be different from wheat
                {
                    Create_Brewmiester_Alert(sample.Batch_id.ToString(), "PH " + sample.Ph.ToString());
                }
                if ( beerType == "Wheat" && (sample.Tank_temp < 28 || sample.Tank_temp > 30))
                {
                    Create_Brewmiester_Alert(sample.Batch_id.ToString(), "PH " + sample.Ph.ToString());
                }

               
            }
            /// Fermentation Alerts
            List<Fermentation> ferment_list = dbs.get_FermentDB();
            foreach (Fermentation fr in ferment_list)
            {
                if (fr.Tank_pressure < 0 || fr.Tank_pressure > 1.3)
                {
                    Create_Brewmiester_Alert(fr.Batchid.ToString() , "Tank Pressure");
                }
            }
            /// Minimum Amount Alerts
            List <Product> prod_list = dbs.get_ProductsDB();
            foreach (Product item in prod_list)
            {
                if (item.Amount < item.Min_amount)
                {
                    Create_Brewmiester_Alert(item.ProductName , "Minimum Amount");
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
                batch_Botteling_List.Sort((p, q) => p.Date.CompareTo(q.Date)); // sort batch list by dates // CHECK THIS

                foreach (var beer_name in beerTypes)
                {
                    int number_of_deviations = 0;
                    int num_of_iterations = 0;
                    foreach (Batch_Botteling batch in batch_Botteling_List)
                    {
                        float avg_waste = batch.AverageWastePercetage(beer_name); // CHECK THIS

                        if (batch.BeerType == beer_name)
                        {
                            num_of_iterations += 1;
                            
                            if (batch.Waste_percent > avg_waste + 3 || batch.Waste_percent < avg_waste - 3) // deviation of +-3% in % waste
                            {
                                number_of_deviations += 1;
                                if (number_of_deviations == 2) 
                                {
                                    Create_Waste_Alert((batch.Waste_percent).ToString(), "Waste Alert"); // more then 2 deviations
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


        private void Create_Brewmiester_Alert(string alert_value, string alertType)
        {
            al.Date = DateTime.Now;
            al.Description = message + alert_value;
            al.Type = alertType;

            al.CreateAlert();
        }
        private void Create_Manager_Alert(string alert_value, string alertType)
        {

        }

        private void Create_Waste_Alert(string alert_value, string alertType) 
        {
            //Create_Brewmiester_Alert(alert_value, alertType);
            //Create_Manager_Alert(alert_value, alertType);
        }
    }
}