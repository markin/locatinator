using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DataSplice.Services.GPS
{
    [ServiceContract(Namespace = "http://DataSplice.Services.GPS")]
    public interface IGPSWebService
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "location",
            ResponseFormat = WebMessageFormat.Json
        )]
        string Location();
    }
}
