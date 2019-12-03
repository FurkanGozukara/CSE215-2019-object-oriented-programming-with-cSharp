using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lecture_13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //threadpools allows you to start as many as threads you want immediately
            ThreadPool.SetMaxThreads(100000, 100000);
            ThreadPool.SetMinThreads(100000, 100000);
            ServicePointManager.DefaultConnectionLimit = 1000;//this increases your number of connections to per host at the same time
        }

        Int64 realValue = 0;

        public class myVals
        {
            public float number1;
            public double number2;
            public int number3;
        }

        myVals myExample = new myVals();

        private void BtnModify_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    Task.Factory.StartNew(() => { incNums(); });
                }
            });

        }

        static int irNumber4;
        private static object objlock = new object();

        private void incNums()
        {
            lock (objlock)
            {
                irNumber4++;
                Interlocked.Increment(ref realValue);
                myExample.number1++;
                myExample.number2++;
                myExample.number3++;
            }
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            string srMsg = "";
            lock (objlock)
            {
                srMsg = $"val 1 : {myExample.number1.ToString("N0")} \t val 2 : {myExample.number2.ToString("N0")} \t val 3 : {myExample.number3.ToString("N0")} \t val 4 : {irNumber4.ToString("N0")} \t real value : {Interlocked.Read(ref realValue).ToString("N0")}";
            }
            lblScreen.Content = srMsg;
        }

        private void BtnUsingExample_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10000; i++)
            {
                Task.Factory.StartNew(() => { taskDownload("https://www.memurlar.net?" + i); });
            }
        }

        private void taskDownload(string srUrl)
        {
            using (WebClient wb = new WebClient())
            {
                string srSource = wb.DownloadString(srUrl);
            }
        }

        static Dictionary<string, int> dicValues = new Dictionary<string, int>();

        private void BtnConqDic_Click(object sender, RoutedEventArgs e)
        {
            Random randGenerator = new Random();

            while (true)
            {
                //   Task.Factory.StartNew(() => { addDic(randGenerator.Next(0, 100000).ToString()); });
                Task.Factory.StartNew(() => { addDicConc(randGenerator.Next(0, 100000).ToString()); });
            }
        }

        private void addDic(string srNumber)
        {
            //this gives error
            if (!dicValues.ContainsKey(srNumber))
            {
                dicValues.Add(srNumber, 1);
            }
            else
                dicValues[srNumber]++;
        }

        ConcurrentDictionary<string, int> dicConc = new ConcurrentDictionary<string, int>();

        private void addDicConc(string srNumber)
        {
            //this gives error
            if (!dicConc.ContainsKey(srNumber))
            {
                bool blResult = dicConc.TryAdd(srNumber, 1);
                if (blResult == false)
                    dicConc[srNumber]++;
            }
            else
                dicConc[srNumber]++;
        }

        static async Task<string> downloadWebPage(string srUrl)
        {
            using (WebClient wb = new WebClient())
            {
                return wb.DownloadString(srUrl);
            }
        }
    }
}
