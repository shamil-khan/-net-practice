using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyWork.Views
{
    public partial class TaskApiView : Window
    {
        readonly static Random _Random = new Random(DateTime.Now.Millisecond);
        public static int NextRandom { get { return _Random.Next(1, 4); } }

        public ObservableCollection<string> Logs { get; set; }

        public TaskApiView()
        {
            InitializeComponent();
            Logs = new ObservableCollection<string>();
            LinkClear.MouseLeftButtonDown += (s, e) => Logs.Clear();
            LinkForeach.MouseLeftButtonDown += (s, e) => ForeachTest();
            LinkFor.MouseLeftButtonDown += (s, e) => ForTest();
            LinkNoThread.MouseLeftButtonDown += (s, e) => NoThread();
            LinkThreadNoAwait.MouseLeftButtonDown += async (s, e) => await ThreadNoAwait();
            LinkThreadOne.MouseLeftButtonDown += async (s, e) => await ThreadOne();
            LinkThreadAll.MouseLeftButtonDown += async (s, e) => await ThreadAll();
            LinkClouser.MouseLeftButtonDown += (s, e) => Clouser();
            ListLogs.ItemsSource = Logs;
            Logs.Add("Log Initialized");
        }
        
        async Task ThreadAll()
        {
            Logs.AddWithThreadId("----------- Start: Thread All Test");
            var count = 100;
            var tasks = new List<Task<string>>(count);
            var id = 1;
            foreach (var each in User.GetUsers(count))
            {
                Logs.AddWithThreadId("start task for user: {0,-5}, id: {1:000}", each.Name, id);
                tasks.Add(LongProcessAsync(each, id++, NextRandom));
            }
            await Task.WhenAll(tasks.ToArray());
            Logs.AddWithThreadId("----------- End: Thread All Test");
        }

        async Task ThreadOne()
        {
            Logs.AddWithThreadId("----------- Start: Thread One Test");
            var id = 1;
            foreach (var each in User.GetUsers(3))
            {
                Logs.AddWithThreadId("start task for user: {0,-5}, id: {1:000}", each.Name, id);
                //var result = await LongProcessAsync(each, id++, NextRandom);
                //Logs.Add(result);
                await LongProcessAsync(each, id++, NextRandom);
            }
            Logs.AddWithThreadId("----------- End: Thread One Test");
        }

        async Task ThreadNoAwait()
        {
            Logs.AddWithThreadId("----------- Start: Thread No Await Test");
            var id = 1;
            foreach (var each in User.GetUsers(3))
            {
                Logs.AddWithThreadId("start task for user: {0,-5}, id: {1:000}", each.Name, id);
                //var result = LongProcessAsync(each, id++, NextRandom);
                //Logs.Add(result.Result);
                LongProcessAsync(each, id++, NextRandom);
            }
            Logs.AddWithThreadId("----------- End: Thread No Await Test");
        }

        void NoThread()
        {
            Logs.AddWithThreadId("----------- Start: Without Thread");
            int id = 1;
            foreach (var each in User.GetUsers(3))
            {
                Logs.AddWithThreadId("start task for user: {0,-5}, id: {1:000}", each.Name, id);
                var result = LongProcess(each, id++, NextRandom);
                Logs.Add(result);
            }
            Logs.AddWithThreadId("----------- End: Without Thread");
        }

        async Task<string> LongProcessAsync(User user, int id, int count)
        {
            return await Task.Run<string>(() =>
                    {
                        var result = LongProcess(user, id, count);
                        this.DispatchLog(result);
                        return result;
                    });
        }

        string LongProcess(User user, int id, int count)
        {
            var seconds = 1000;
            var builder = new StringBuilder(5);
            builder.AppendFormat("[({0:hh.mm.ss}) Start - ThreadId: {1:000}, User: {2,-5}, Id: {3:000}", DateTime.Now, Thread.CurrentThread.ManagedThreadId, user.Name, id);
            Debug.WriteLine(builder.ToString());
            Thread.Sleep(seconds * count);
            builder.AppendFormat(" - Waited {0,2}s", count);
            builder.AppendFormat(" - Stop ({0:hh.mm.ss})]", DateTime.Now);
            Debug.WriteLine(builder.ToString());
            return builder.ToString();
        }

        void Clouser()
        {
            Logs.AddWithThreadId("----------- Start: Clouser Test");
            var actions = new List<Func<int, int>>(3);
            for(int i=0; i<3; i++)
            {
                //actions.Add((n1) => n1 * i);
                var n2 = i;
                actions.Add((n1) => n1 * n2);
            }

            foreach (var each in actions)
            {
                Logs.AddWithThreadId("1 * i = {0}", each(1));
            }
            Logs.AddWithThreadId("----------- End: Clouser Test");
        }

        private void ForeachTest()
        {
            Logs.AddWithThreadId("----------- Start: Foreach Test");
            var names = new List<string>(new[] {"ali", "tom", "zia"});
            foreach (string each in names)
            {
                //each = "harry"; // cant compile
                Logs.Add(each);
                //if(each == "tom") names.Add("khan"); //cant allow, throws exception
            }
            Logs.AddWithThreadId("----------- End: Foreach Test");
        }

        private void ForTest()
        {
            Logs.AddWithThreadId("----------- Start: For Test");
            var names = new List<string>(new[] { "ali", "tom", "zia" });
            for (var i = 0; i < names.Count; i++)
            {
                names[i] = string.Format("{0,-5} {1:00}", names[i], i);
                Logs.Add(names[i]);
                if (i == 1) names.Add("khan");
            }
            Logs.AddWithThreadId("----------- End: For Test");
        }
        
    }

    public static class LogExtension
    {
        public static void AddWithThreadId(this ObservableCollection<string> items, string message, params object[] args)
        {
            var value = string.Format(message, args);
            var log = string.Format("{0}, [{1:hh.mm.ss} - Current-ThreadId: {2:000}]", value, DateTime.Now, Thread.CurrentThread.ManagedThreadId);
            Debug.WriteLine(log);
            items.Add(log);
        }

        public static void DispatchLog(this TaskApiView view, string message, params object[] args)
        {
            view.Dispatcher.Invoke(new Action(() =>
                    {
                        view.Logs.AddWithThreadId(message, args);
                    }));

        }

    }

    public class User
    {
        public string Name { get; set; }

        public static IEnumerable<User> GetUsers(int count)
        {
            var items = new List<User>(count);
            for(int i=0; i<count; i++)
            {
                items.Add(new User() { Name = "u" + (i + 1)});
            }
            return items;
        }
    }
}