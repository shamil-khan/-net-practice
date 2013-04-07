using MyWork.ServiceReference1;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyWork.Views
{
    public partial class SecureServiceView : Window
    {
        ObservableCollection<string> _Logs;

        public SecureServiceView()
        {
            InitializeComponent();
            
            _Logs = new ObservableCollection<string>();
            ListResponse.ItemsSource = _Logs;
            LinkClear.MouseLeftButtonDown += (s, e) => _Logs.Clear(); 
            LinkGetNow.MouseLeftButtonDown += (s, e) => GetNow();
            LinkEcho.MouseLeftButtonDown += (s, e) => Echo();
            _Logs.Add("Start");
        }


        void GetNow()
        {
            var client = new EchoServiceClient();
            var r = client.GetNow();
            _Logs.Add(r);
        }

        void Echo()
        {
            var client = new EchoServiceClient();
            var r = client.Echo(TextRequest.Text);
            _Logs.Add(r);
        }

    }
}
