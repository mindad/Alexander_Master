using Alexander.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexander.Models
{
    public class Brew
    {
        private int batch_id;
        private List<Product> prod_list;

        public int Batch_id { get => batch_id; set => batch_id = value; }
        public List<Product> Prod_list { get => prod_list; set => prod_list = value; }

        public Brew(int batch_id, List<Product> prod_list)
        {
            this.Batch_id = batch_id;
            this.Prod_list = prod_list;
        }
        public Brew()
        {
        }

        public List<Brew> get_Brews()
        {
            DBservices dbs = new DBservices();

            List<Brew> Brews_arr = dbs.get_BrewDB();

            return Brews_arr;
        }

        public int insert() // insert new row into Product_2020
        {
            int numEffected = 0;
            DBservices dbs = new DBservices();
            string st="";


            try
            {
                foreach (var prod in prod_list)
                {
                    if (prod_list.IndexOf(prod) == prod_list.Count - 1) // Last item in iteration
                    {
                        st += prod.ProductName + ":" + prod.Amount;
                        break;
                    }
                    st += prod.ProductName + ":" + prod.Amount + ",";
                }
            }
            catch (NullReferenceException ex) // prod_list = ""
            {
            }

            try
            {
                dbs = dbs.read("[Brew_2020]");

                dbs.dt.Rows.Add(Batch_id, st);

                numEffected = dbs.update();
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(string.Format("Unable to insert batch_id: {0} to table", Batch_id), ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return numEffected;
        }
    }
}