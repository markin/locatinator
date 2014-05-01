using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration.Install;
using System.ComponentModel;
using System.ServiceProcess;

namespace DataSplice.Services.GPS
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "DataSpliceGPSService";
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}