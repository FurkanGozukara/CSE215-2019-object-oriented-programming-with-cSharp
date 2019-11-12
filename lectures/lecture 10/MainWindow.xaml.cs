using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
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


        //Action in C# represents a delegate that has void return type and optional parameters. There are two variants of Action delegate.
        //http://dotnetpattern.com/csharp-action-delegate
        private void what_is_action(object sender, RoutedEventArgs e)
        {
            Action doWorkAction = new Action(DoWork);
            doWorkAction(); //Print "Hi, I am doing work."
            Action<int> firstAction = DoWorkWithOneParameter;
            Action<int, int> secondAction = DoWorkWithTwoParameters;
            Action<int, int, int> thirdAction = DoWorkWithThreeParameters;

            firstAction(1); // Print 1
            secondAction(1, 2); // Print 1-2
            thirdAction(1, 2, 3); //Print 1-2-3


            Action act = () =>
            {
                Debug.WriteLine("No Parameter");
            };

            Action<int> actWithOneParameter = (arg1) =>
            {
                Debug.WriteLine("Par: " + arg1);
            };

            Action<int, int> actWithTwoParameter = (arg1, arg2) =>
            {
                Debug.WriteLine("Par1: " + arg1 + ", Par2: " + arg2);
            };



            act();
            actWithOneParameter(1);
            actWithTwoParameter(1, 2);

            this.Dispatcher.BeginInvoke(
                   actWithTwoParameter,new [] { 66,77,55});
        }

        static Int64 irNum;

        public static void DoWork()
        {
            irNum++;
        }

        public static void DoWorkWithOneParameter(int arg)
        {
            Debug.WriteLine(arg);
        }

        public static void DoWorkWithTwoParameters(int arg1, int arg2)
        {
            Debug.WriteLine(arg1 + "-" + arg2);
        }

        public static void DoWorkWithThreeParameters(int arg1, int arg2, int arg3)
        {
            Debug.WriteLine(arg1 + "-" + arg2 + "-" + arg3);
        }
    }
}
