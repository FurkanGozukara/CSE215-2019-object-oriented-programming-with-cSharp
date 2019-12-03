using System;
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
            Task.Factory.StartNew(() => {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    Task.Factory.StartNew(() => { incNums(); });
                }
            });
         
        }

        static int irNumber4;

        private void incNums()
        {
            irNumber4++;
            Interlocked.Increment(ref realValue);
            myExample.number1++;
            myExample.number2++;
            myExample.number3++;
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            string srMsg = $"val 1 : {myExample.number1.ToString("N0")} \t val 2 : {myExample.number2.ToString("N0")} \t val 3 : {myExample.number3.ToString("N0")} \t val 4 : {irNumber4.ToString("N0")} \t real value : {Interlocked.Read(ref realValue).ToString("N0")}";
            lblScreen.Content = srMsg;
        }
    }
}
