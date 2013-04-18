using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace MyWork.Services
{
    [ServiceContract]
    public interface IEchoWcfService
    {
        [OperationContract]
        string Echo(string value);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EchoWcfService : IEchoWcfService
    {
        public string Echo(string value)
        {
            return string.Format("({0:hh.mm.ss.ffff}) Echo-Wcf - {1}", DateTime.Now, value);
        }
    }
}
