using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;


namespace Alexander.Models
{
    public class AlertsManager
    {

        private int alertID;
        private string type;
        private DateTime date;
        private string description;
        private string notes;

        public AlertsManager() { }

        public AlertsManager(int alertID, string type, DateTime date, string description, string notes)
        {
            this.alertID = alertID;
            this.type = type;
            this.date = date;
            this.description = description;
            this.notes = notes;
        }

        public int AlertID { get => alertID; set => alertID = value; }
        public string Type { get => type; set => type = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Description { get => description; set => description = value; }
        public string Notes { get => notes; set => notes = value; }


        public List<AlertsManager> get_AlertsManager()
        {
            DBservices dbs = new DBservices();

            List<AlertsManager> alertManager_arr = dbs.get_AlertsManagerDB();

            return alertManager_arr;
        }

        public int Update(int Alert_id, string alert_notes)
        {
            int effected = 0;
            DBservices dbs = new DBservices();

            dbs = dbs.read("[Alert_Manager_2020]");

            DataRow dr = dbs.dt.Select("Alert_id=" + Alert_id).First(); // gets the row where id == batchid

            //dbs.dt = edit(dbs.dt, this.batchID);
            dr["notes"] = alert_notes;

            effected = dbs.update(); // update DB

            return effected;
        }

        public int delete_line(int row)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Alert_Manager_2020]");

            try
            {
                dbs.dt.Select("Alert_id=" + row).First().Delete(); // Delete a line in DataTable
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 1;
        }






    }
}
