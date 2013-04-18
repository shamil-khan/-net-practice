using System;
using System.ServiceModel;
using System.Threading;
using MyWork.ServiceReference1;
using MyWork.ServiceReference2;
using MyWork.ServiceReference3;
using MyWork.Services;

namespace MyWork.ConsoleClient
{
    class Program : IEchoDuplexServiceCallback
    {
        static void WriteException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void Main(string[] args)
        {
            WriteException(() =>
            {
                Program main = new Program();

                Console.WriteLine("===============================================");
                main.ShowEchoRiaService();
                main.ShowLog();


                Console.WriteLine("===============================================");
                main.ShowEchoWcfService();
                main.ShowLog();

                Console.WriteLine("{0}{0}===============================================", Environment.NewLine);
                main.ShowEchoDuplexService();
                main.ShowLog();

                Console.WriteLine("-----------------------------------------------{0}{0}", Environment.NewLine);
            });
            Console.Write("Press <<Enter>> key to exit ...");
            Console.ReadLine();
        }

        MessageLogInspector _Log = new MessageLogInspector();

        void ShowEchoRiaService()
        {
            EchoRiaServiceSoapClient service = new EchoRiaServiceSoapClient("BasicHttpBinding_EchoRiaServiceSoap");
            service.Endpoint.Behaviors.Add(_Log);
            Console.WriteLine("Echo Ria Service Result : " + service.Echo("Hello"));
        }

        void ShowEchoWcfService()
        {
            EchoWcfServiceClient service = new EchoWcfServiceClient("BasicRelative");
            service.Endpoint.Behaviors.Add(_Log);
            Console.WriteLine("Echo Wcf Service Result : " + service.Echo("Hello"));
        }

        void ShowEchoDuplexService()
        {
            EchoDuplexServiceClient service = new EchoDuplexServiceClient(new InstanceContext(this), "DualHttpRelative");
            service.Endpoint.Behaviors.Add(_Log);
            Console.WriteLine("Echo Duplex Service Result : " + service.Echo("Hello"));
            Console.WriteLine("Echo Duplex Service Register : " + service.Register("HelloConsole"));
            Thread.Sleep(10 * 1000);
            Console.WriteLine("Echo Duplex Service Un-Register : " + service.UnRegister("HelloConsole"));
        }

        void ShowLog()
        {
            Console.WriteLine("------------------- Request -------------------");
            Console.WriteLine(_Log.Request);
            Console.WriteLine("------------------- Response -------------------");
            Console.WriteLine(_Log.Response);
        }

        public void Receive(string value)
        {
            Console.WriteLine("Echo Duplex Service Recieve : " + value);
        }


    }

}
