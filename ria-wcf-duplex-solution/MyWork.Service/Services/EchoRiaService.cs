using System;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;

namespace MyWork.Services
{
    [EnableClientAccess()]
    public class EchoRiaService : DomainService
    {
        public string Echo(string value)
        {
            return string.Format("({0:hh.mm.ss.ffff}) Echo-Ria - {1}", DateTime.Now, value);
        }
    }
}


