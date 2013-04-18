using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Sky.Services
{
    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        [WebGet(UriTemplate = "echo/{value}", BodyStyle = WebMessageBodyStyle.Bare)]
        string Echo(string value);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EchoService : IEchoService
    {
        public string Echo(string value)
        {
            return string.Format("({0:hh.mm.ss.ffffff}) Echo - {1} ", DateTime.Now, value);
        }
    }
}
