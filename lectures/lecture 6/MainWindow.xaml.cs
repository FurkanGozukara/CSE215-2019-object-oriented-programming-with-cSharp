using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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


namespace lecture_6
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

        private void exception_handle(object sender, RoutedEventArgs e)
        {
            int irCase = 5;

            try
            {
                switch (irCase)
                {
                    default:
                        throw new Exception();
                        break;
                    case 1:
                        throw new IndexOutOfRangeException();
                        break;
                    case 2:
                        throw new FormatException();
                        break;
                    case 3:
                        throw new InvalidCastException();
                        break;
                }
            }
            catch (IndexOutOfRangeException E)
            {
                MessageBox.Show("catched IndexOutOfRangeException");

            }
            catch (FormatException E)
            {
                MessageBox.Show("catched FormatException");
            }
            catch (InvalidCastException E)
            {
                MessageBox.Show("catched InvalidCastException");
            }
            catch (Exception E)
            {
                MessageBox.Show("catched uncaught exception");
            }

            try
            {
                File.ReadAllText("gg.txt");
            }
            catch (ArgumentException E)
            {
                //solution for ArgumentException
            }
            catch (PathTooLongException E)
            {
                //solution for PathTooLongException
            }
            catch (DirectoryNotFoundException E)
            {
                //solution for DirectoryNotFoundException
            }
            catch (Exception E)
            {
                //solution for all other exception
            }

        }

        private void BtnThisVsBase_Click(object sender, RoutedEventArgs e)
        {
            csChild gg = new csChild();
            gg.function1();
            gg.function2();
        }

        public class csParent
        {
            public string srName = "from csParent";
        }

        public class csChild : csParent
        {
            public string srName = "from csChild";

            public void function1()
            {
                MessageBox.Show(this.srName);//this keyword references to currently instanced class
            }

            public void function2()
            {
                MessageBox.Show(base.srName);//base keyword references to parent(inherited from) class
            }
        }

        private void BtnAbstracClass_Click(object sender, RoutedEventArgs e)
        {
            csAbstract.Square mySquare = new csAbstract.Square(32);
            MessageBox.Show(mySquare.area().ToString());
        }

        private void function_overload(object sender, RoutedEventArgs e)
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();

            string srResults1 = "";

            for (int i = 0; i < 10000; i++)
            {
                srResults1 += csFunctions.returnFormattedString(Convert.ToDouble(i)) + "\r\n";
                srResults1 += csFunctions.returnFormattedString(i) + "\r\n";
                srResults1 += csFunctions.returnFormattedString(Convert.ToInt64(i)) + "\r\n";
                srResults1 += csFunctions.returnFormattedString(i, Convert.ToDouble(i)) + "\r\n";
                srResults1 += csFunctions.returnFormattedString(Convert.ToDouble(i), i) + "\r\n";
            }

            File.WriteAllText("method_1.txt", srResults1);

            timer.Stop();

            MessageBox.Show("regular string addition method took " + timer.ElapsedMilliseconds + " MS");

            timer.Reset();

            timer.Start();

            StringBuilder srBuild = new StringBuilder();

            for (int i = 0; i < 10000; i++)
            {
                srBuild.AppendLine(csFunctions.returnFormattedString(Convert.ToDouble(i)));
                srBuild.AppendLine(csFunctions.returnFormattedString(i));
                srBuild.AppendLine(csFunctions.returnFormattedString(Convert.ToInt64(i)));
                srBuild.AppendLine(csFunctions.returnFormattedString(i, Convert.ToDouble(i)));
                srBuild.AppendLine(csFunctions.returnFormattedString(Convert.ToDouble(i), i));
            }

            File.WriteAllText("method_2.txt", srBuild.ToString());

            timer.Stop();

            MessageBox.Show("string builder string addition method took " + timer.ElapsedMilliseconds + " MS");
        }

        private void method_overload(object sender, RoutedEventArgs e)
        {

            Box Box1 = new Box();   // Declare Box1 of type Box
            Box Box2 = new Box();   // Declare Box2 of type Box
            Box Box3 = new Box();   // Declare Box3 of type Box
            double volume = 0.0;    // Store the volume of a box here

            // box 1 specification
            Box1.setLength(6.0);
            Box1.setBreadth(7.0);
            Box1.setHeight(5.0);

            // box 2 specification
            Box2.setLength(12.0);
            Box2.setBreadth(13.0);
            Box2.setHeight(10.0);

            // volume of box 1
            volume = Box1.getVolume();
            MessageBox.Show(string.Format("Volume of Box1 : {0}", volume));

            // volume of box 2
            volume = Box2.getVolume();
            MessageBox.Show(string.Format("Volume of Box2 : {0}", volume));

            // Add two object as follows:
            Box3 = Box1 + Box2;

            // volume of box 3
            volume = Box3.getVolume();
            MessageBox.Show(string.Format("Volume of Box3 : {0}", volume));

            var box4 = Box1 + Box2 + Box3;//operator overloading advantage

            volume = box4.getVolume();
            MessageBox.Show(string.Format("Volume of Box4 : {0}", volume));

            Box3 = Box.sum_boxes(Box1, Box2);//this is same as Box1+Box2

        }

        private void dynamic_poli_example(object sender, RoutedEventArgs e)
        {
            // Polymorphism at work #1: a Rectangle, Triangle and Circle
            // can all be used whereever a Shape is expected. No cast is
            // required because an implicit conversion exists from a derived 
            // class to its base class.
            var shapes = new List<customShapes.Shape>
        {
            new customShapes.Rectangle(),
            new customShapes.Triangle(),
            new customShapes.Circle()
        };

            // Polymorphism at work #2: the virtual method Draw is
            // invoked on each of the derived classes, not the base class.
            foreach (var shape in shapes)
            {
                shape.Draw();
            }

        }
    }
}
