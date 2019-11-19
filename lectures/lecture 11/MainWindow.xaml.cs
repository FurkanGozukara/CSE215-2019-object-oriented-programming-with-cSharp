﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace lecture_11
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

        private void BtnNewThread_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(updateText);
            thread.Start();
        }

        private void updateText()
        {
            while (true)
            {
                lblClock.Dispatcher.BeginInvoke(new Action(() =>
                {
                    lblClock.Content = DateTime.Now.TimeOfDay;
                }));
                System.Threading.Thread.Sleep(500);
            }
        }

        private void BtnMainThreadcall_Click(object sender, RoutedEventArgs e)
        {
            updateText();
        }

        //task vs thread > http://www.albahari.com/threading/
        private void BtnTaskStart_Click(object sender, RoutedEventArgs e)
        {
            var task = Task.Factory.StartNew(new Action(() =>
            {
                updateText();
            }));
            
        }

        // taking a class 
        public class P { }

        // taking a class 
        // derived from P 
        public class P1 : P { }

        // taking a class 
        public class P2 { }

        //https://www.geeksforgeeks.org/is-vs-as-operator-keyword-in-c-sharp/
        private void BtnIsOp_Click(object sender, RoutedEventArgs e)
        {
            // creating an instance 
            // of class P 
            P o1 = new P();

            // creating an instance 
            // of class P1 
            P1 o2 = new P1();

            // checking whether 'o1' 
            // is of type 'P' 
            Debug.WriteLine(o1 is P);

            // checking whether 'o1' is 
            // of type Object class 
            // (Base class for all classes) 
            Debug.WriteLine(o1 is Object);

            // checking whether 'o2' 
            // is of type 'P1' 
            Debug.WriteLine(o2 is P1);

            // checking whether 'o2' is 
            // of type Object class 
            // (Base class for all classes) 
            Debug.WriteLine(o2 is Object);

            // checking whether 'o2' 
            // is of type 'P' 
            // it will return true as P1 
            // is derived from P 
            Debug.WriteLine(o2 is P1);

            // checking whether o1 
            // is of type P2 
            // it will return false 
            Debug.WriteLine(o1 is P2);

            // checking whether o2 
            // is of type P2 
            // it will return false 
            Debug.WriteLine(o2 is P2);


        }

        class Y { }
        class Z { }

        private void Btnasoper_Click(object sender, RoutedEventArgs e)
        {
            object[] o = new object[5];
            o[0] = new Y();
            o[1] = new Z();
            o[2] = "Hello";
            o[3] = 4759.0;
            o[4] = null;

            for (int q = 0; q < o.Length; ++q)
            {
                // using as operator 
                string str1 = o[q] as string;
    
                Debug.WriteLine(o[q]?.ToString());

                // checking for the result 
                if (str1 != null)
                {
                    Debug.WriteLine("'" + str1 + "'");
                 
                }
                else
                {
                    Debug.WriteLine("Is is not a string");
                }
            }
        }
    }
}
