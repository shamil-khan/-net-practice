using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Timers;
using System.Collections.Generic;

namespace MyWork.Services
{
    [ServiceContract]
    public interface IEchoDuplexClient
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string value);
    }

    [ServiceContract(Namespace = "Silverlight", CallbackContract = typeof(IEchoDuplexClient))]
    public interface IEchoDuplexService
    {
        [OperationContract]
        string Echo(string Echo);


        [OperationContract]
        bool Register(string id);

        [OperationContract]
        bool UnRegister(string id);

    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EchoDuplexService : IEchoDuplexService
    {
        Dictionary<string, IEchoDuplexClient> _Clients = new Dictionary<string, IEchoDuplexClient>();
        Timer _Timer;

        public bool Register(string id)
        {
            var client = OperationContext.Current.GetCallbackChannel<IEchoDuplexClient>();
            _Clients.Add(id, client);
            _Timer = new Timer(1000);
            _Timer.Elapsed += _Timer_Elapsed;
            _Timer.Enabled = true;
            _Timer.Start();
            return true;
        }

        public bool UnRegister(string id)
        {
            if (_Timer != null)
            {
                _Timer.Stop();
                _Timer.Dispose();
                _Timer = null;
            }
            if (_Clients.ContainsKey(id)) _Clients.Remove(id);
            return true;
        }

        public void _Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_Clients == null || _Clients.Count == 0) return;
            foreach (var each in _Clients)
            {
                each.Value.Receive(string.Format("({0:hh.mm.ss.ffff}) Echo-Duplex Recevied Called - {1}", DateTime.Now, each.Key));
            }
        }

        public string Echo(string value)
        {
            return string.Format("({0:hh.mm.ss.ffff}) Echo-Duplex - {1}", DateTime.Now, value);
        }
    }

}
