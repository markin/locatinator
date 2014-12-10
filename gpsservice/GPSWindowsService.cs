using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceProcess;

namespace DataSplice.Services.GPS
{
    public class GPSWindowsService : ServiceBase
    {
        public ServiceHost serviceHost = null;
        public GPSWindowsService()
        {
            // Name the Windows Service
            ServiceName = "DataSplice GPS Service";
        }

        public static void Main()
        {
            ServiceBase.Run(new GPSWindowsService());
        }

        // Start the Windows service.
        protected override void OnStart(string[] args)
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            // Create a ServiceHost for the GPSWebService type and 
            // provide the base address.
            IGPSWebService webService = new GPSWebService();
            serviceHost = new ServiceHost(webService);
            //serviceHost = new ServiceHost(typeof(GPSWebService));

            // Open the ServiceHostBase to create listeners and start 
            // listening for messages.
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
