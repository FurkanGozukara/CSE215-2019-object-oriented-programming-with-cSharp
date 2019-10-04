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
using myCustomMethodExtensions;
using nmStudent;
//using nmStudent2;
using static week_1_hello_object_oriented_world.csPublicFunctions.constantVariables;
using static week_1_hello_object_oriented_world.csPublicFunctions;

namespace week_1_hello_object_oriented_world
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //loadStudentsPrimitive();
            loadProperly(ref dicStudents);
            refreshListBox();
            lblSelectedStudent.Content = "";
            changeVisibility(false);
        }

        private void changeVisibility(bool blTrue = true)
        {
            if (txtCourseName.Visibility == Visibility.Visible)
            {
                if (blTrue == true)
                    return;
                txtCourseName.Visibility = Visibility.Hidden;
            }
            else
        if (txtCourseName.Visibility != Visibility.Visible)
                txtCourseName.Visibility = Visibility.Visible;

            if (txtCourseScore.Visibility == Visibility.Visible)
                txtCourseScore.Visibility = Visibility.Hidden;
            else
            if (txtCourseScore.Visibility != Visibility.Visible)
                txtCourseScore.Visibility = Visibility.Visible;
        }

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            string srResult = "";
            bool blResult = addStudent(out srResult, txtStudentId.Text, txtStudentName.Text);
            if (blResult == false)
            {
                MessageBox.Show(srResult);
                return;
            }
            refreshListBox();
        }

        private void refreshListBox()
        {
            lstStudentsList.Items.Clear();

            csPublicFunctions.dicStudents = dicStudents.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            foreach (var vrPerStudent in dicStudents.Values)
            {
                var vrValue = string.Format("Id: {0}\t{1}", vrPerStudent.irStudentId.ToString("N0"), vrPerStudent.srStudentName);
                foreach (var vrLesson in vrPerStudent.lstLessons)
                {
                    vrValue += "\t" + vrLesson.srLessonName + " : " + vrLesson.irFinalScore;
                }
                lstStudentsList.Items.Add(vrValue);
            }

            //sort dictionary by student id first
            //then add student information to the listbox
        }

        //call this when the application starts
  
        static int irSelectedStudentNo = 0;

        private void BtnAddCourse_Click(object sender, RoutedEventArgs e)
        {
            csPublicFunctions.addCourse(irSelectedStudentNo, txtCourseScore.Text, txtCourseName.Text);
            refreshListBox();
        }

        private void LstStudentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ListBox)sender;
            if (item.SelectedItem == null)
                return;
            var selectedStudent = item.SelectedItem.ToString().Replace("Id: ", "");
            irSelectedStudentNo = Convert.ToInt32(selectedStudent.Split('\t').First());
            changeVisibility();
        }
    }
}
