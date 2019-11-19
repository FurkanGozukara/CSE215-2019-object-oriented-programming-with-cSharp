using System;
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

        //https://www.geeksforgeeks.org/c-sharp-type-casting/
        private void BtnTypeCasting_Click(object sender, RoutedEventArgs e)
        {
            int i = 57;

            // automatic type conversion implict
            long l = i;

            // automatic type conversion 
            float f = l;

            // Display Result 
            Debug.WriteLine("Int value " + i);
            Debug.WriteLine("Long value " + l);
            Debug.WriteLine("Float value " + f);

            double d = 765.12;

            // Incompatible Data Type 
            //int i = d;//this gives error cannot implicty convert double to int

            double gg = i;

            double ga = 765.12;

            // Explicit Type Casting 
            int ii = (int)d;

            // Display Result 
            Console.WriteLine("Value of i is " + ii);


            int ia = 12;
            double daa = 765.12;
            float faa = 56.123F;

            // Using Built- In Type Conversion 
            // Methods & Displaying Result 
            Debug.WriteLine(Convert.ToString(faa));
            Debug.WriteLine(Convert.ToInt32(daa));
            Debug.WriteLine(Convert.ToUInt32(faa));
            Debug.WriteLine(Convert.ToDouble(ia));

        }

        //https://www.tutorialsteacher.com/csharp/csharp-interface
        interface IPen
        {
            string Color { get; set; }
            bool Open();
            bool Close();
            void Write(string text);
        }

        class Cello : IPen
        {
            public string Color { get; set; }

            private bool isOpen = false;

            public bool Close()
            {
                isOpen = false;
                Debug.WriteLine("Cello closed for writing!");

                return isOpen;
            }

            public bool Open()
            {
                isOpen = true;
                Debug.WriteLine("Cello open for writing!");

                return isOpen;
            }

            public void Write(string text)
            {
                //write text if open
                if (isOpen)
                    Debug.WriteLine("Cello: " + text);
            }
        }

        class Parker : IPen
        {
            public string Color { get; set; }

            private bool canWrite = false;

            public bool Close()
            {
                canWrite = false;
                Debug.WriteLine("Parker is closed now!");

                return canWrite;
            }

            public bool Open()
            {
                canWrite = true;
                Debug.WriteLine("Parker is open now!");

                return canWrite;
            }

            public void Write(string text)
            {
                //write text if open
                if (canWrite)
                    Debug.WriteLine("Parker: " + text);
            }
        }

        interface IBrandedPen
        {
            string GetBrandName();
        }

        class Parker2 : IPen, IBrandedPen
        {
            public string Color { get; set; }

            public string GetBrandName()
            {
                return "Parker2";
            }

            private bool canWrite = false;

            public bool Close()
            {
                canWrite = false;
                Debug.WriteLine("Parker2 is closed now!");

                return canWrite;
            }

            public bool Open()
            {
                canWrite = true;
                Debug.WriteLine("Parker is open now!");

                return canWrite;
            }

            public void Write(string text)
            {
                //write text if open
                if (canWrite)
                    Debug.WriteLine("Parker: " + text);
            }
        }

        private void BtnInterface_Click(object sender, RoutedEventArgs e)
        {
            IPen pen1 = new Cello();

            IPen pen2 = new Parker();

            pen1.Open();
            pen2.Open();

            IPen pen3 = new Parker2();
            //pen3.GetBrandName()//this gives error


            Parker2 pen4 = new Parker2();
            pen4.GetBrandName();//this works
        }

       // Example: Interface Inheritance
//interface IPen
//        {
//            string Color { get; set; }
//            bool Open();
//            bool Close();
//            void Write(string text);
//        }

//        interface IBrandedPen : IPen
//        {
//            string GetBrandName();
//        }

//        class Parker : IBrandedPen
//        {
//            //Implement all members of IPen and IBrandedPen
//        }
    }
}
