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
    }
}
