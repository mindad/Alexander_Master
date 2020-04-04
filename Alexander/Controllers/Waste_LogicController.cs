using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Text;
using System.Threading;
//using System.IO;


namespace Alexander.Controllers
{
    public class Waste_LogicController : ApiController
    {

        public string Get()
        {
            try
            {
                // replace with python PATH and send the relevant script in it
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = @"C:\Windows\System32\cmd.exe";//
                start.Arguments = @"/C mkdir C:\Users\Owner\Desktop\asd";// /c is needed !!!
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = false;
                start.WindowStyle = ProcessWindowStyle.Normal;
                using (Process process = Process.Start(start))
                {
                    //process.Kill(); // might be needed for killing the py file
                    process.WaitForExit();
                }

                return "Made asd Dir";
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        

    }
}
