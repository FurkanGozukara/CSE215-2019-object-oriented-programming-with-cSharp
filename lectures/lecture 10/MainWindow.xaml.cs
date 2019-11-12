using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace lecture_10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Convert.ToInt32("asd31");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {   //this does not cause application termination because it is not in main UI thread
            Task.Factory.StartNew((Action)(() =>
            {
                Convert.ToInt32("asd31");
            }));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                Convert.ToInt32("asd31");
            }));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //this updates UI only after for loop ended 
            for (int i = 0; i < int.MaxValue; i++)
            {
                lblNumbers.Content = i;
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //this starts a sub thread
            Task.Factory.StartNew((Action)(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    //this updates UI whenever possible
                    this.Dispatcher.BeginInvoke(
                             new Action(() =>
                             {
                                 lblNumbers.Content = i;
                             }));
                    System.Threading.Thread.Sleep(1000);
                }
            }));
        }
    }
}
