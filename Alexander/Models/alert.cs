using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;


namespace Alexander.Models
{
    public class alert
    {
        private int alertID;
        private string type;
        private DateTime date;
        private string description;
        private string notes;

        public alert() { }

        public alert(int alertID, string type, DateTime date, string description, string notes)
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


        public List<alert> get_Alerts()
        {
            DBservices dbs = new DBservices();

            List<alert> alert_arr = dbs.get_AlertsDB();

            return alert_arr;
        }

        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            //dbs = dbs.read_batches();

            //dbs.dt = edit(dbs.dt, this.batchID);

            effected = dbs.update(); // update DB

            return effected;
        }

    }
}