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
//2019103004
using custom_delagates;
using static custom_delagates.delegateExample;

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

        private void main_thread_error(object sender, RoutedEventArgs e)
        {
            Convert.ToInt32("asd31");
        }

        private void sub_thread_error_non_terminates(object sender, RoutedEventArgs e)
        {   //this does not cause application termination because it is not in main UI thread
            Task.Factory.StartNew((Action)(() =>
            {
                Convert.ToInt32("asd31");
            }));
        }

        private void main_thread_error_terminates(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                Convert.ToInt32("asd31");
            }));
        }

        private void UI_blocking_UI_update(object sender, RoutedEventArgs e)
        {
            //this updates UI only after for loop ended 
            for (int i = 0; i < int.MaxValue; i++)
            {
                lblNumbers.Content = i;
                System.Threading.Thread.Sleep(1);
            }
        }

        private void non_thread_blocking_UI_update(object sender, RoutedEventArgs e)
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
                    System.Threading.Thread.Sleep(1);
                }
            }));
        }

        private void delegate_example(object sender, RoutedEventArgs e)
        {
            NumberChanger nc1 = new NumberChanger(AddNum);
            NumberChanger nc2 = new NumberChanger(MultNum);

            MessageBox.Show(getNum().ToString());
            nc1(5);
            MessageBox.Show(getNum().ToString());
            nc2(5);
            MessageBox.Show(getNum().ToString());
        }

        private void multi_cast_delegate(object sender, RoutedEventArgs e)
        {
            NumberChanger nc = new NumberChanger(AddNum);
            nc += new NumberChanger(MultNum);
            MessageBox.Show(getNum().ToString());
            nc(5);
            MessageBox.Show(getNum().ToString());
        }
    }
}
