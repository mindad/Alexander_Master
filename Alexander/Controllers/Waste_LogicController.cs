using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.IO;
//using System.IO;


namespace Alexander.Controllers
{
    public class Waste_LogicController : ApiController
    {

        public string Get()
        {
            try
            {
                string result;
                // replace with python PATH and send the relevant script in it
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = @"C:\Users\Owner\AppData\Local\Programs\Python\Python37-32\python.exe";//
                start.Arguments = @"C:\Users\Owner\Desktop\waste.py";// 
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                start.CreateNoWindow = false;
                start.WindowStyle = ProcessWindowStyle.Normal;

                start.RedirectStandardError = true; // process.StandardError

                using (Process process = Process.Start(start))
                {
                    //process.Kill(); // might be needed for killing the py file
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
                        result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
                        
                    }

                    process.WaitForExit();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        

    }
}
