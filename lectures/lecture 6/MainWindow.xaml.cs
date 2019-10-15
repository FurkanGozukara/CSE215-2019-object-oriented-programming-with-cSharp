using System;
using System.Collections.Generic;
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
            catch(Exception E)
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
    }
}
