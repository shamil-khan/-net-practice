using System;
using System.ServiceModel;

namespace MyWork.Services
{

    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string value);

        [OperationContract]
        string GetNow();

    }

    public class EchoService : IEchoService
    {
        public string Echo(string value)
        {
            return string.Format("({0:hh.mm.ss.ffffff}) - t1 - {1}", DateTime.Now, value);
        }

        public string GetNow()
        {
            return string.Format("{0:hh.mm.ss.ffffff}", DateTime.Now);
        }

    }
}
