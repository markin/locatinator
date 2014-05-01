using DataSplice.Services.GPS;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testservice
{
    public class RunTest
    {
        public static void Main(String[] args)
        {
            GPSWebService webService = new GPSWebService();
            string position = "UNKNOWN";
            while (position == "UNKNOWN")
            {
                position = webService.Location();
                System.Diagnostics.Trace.WriteLine("Position: " + position);
            }
            // Got a known position!
            var knownPosition = position;
            System.Diagnostics.Trace.WriteLine("Known Position: " + knownPosition);
        }
    }
}
