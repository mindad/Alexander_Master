using Alexander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Alexander.Controllers
{
    public class SystemAlertsController : ApiController
    {

        public void Get()
        {
            SystemAlerts sys_alerts = new SystemAlerts();
            sys_alerts.Check_For_Brewmiester_Alerts();
            sys_alerts.Check_For_Waste_Alerts();
        }

    }
}
