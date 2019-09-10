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
        }

        private class csStudent
        {
            public csStudent(int _studentId = 0, string _StudentName = "")
            {
                irStudentId = _studentId;
                srStudentName = _StudentName;
            }

            public csStudent()
            {
                irStudentId = 1;
                srStudentName = "a";
            }

            public int irStudentId = 0;
            public string srStudentName = "";
        }

        private class csLesson
        {
            private int _irLessonId = 0;

            public int irLessonId
            {
                get { return _irLessonId; }
                set { _irLessonId = value; }
            }

            private string _srLessonName = "";

            public string srLessonName
            {
                get { return _srLessonName; }
                set { _srLessonName = value; }
            }

            private int _irFinalScore = 0;

            public int irFinalScore
            {
                get { return _irFinalScore; }
                set
                {
                    _irLessonId = value;
                    if (value > 100)
                        _irFinalScore = 100;
                    if (value < 0)
                        _irFinalScore = 0;
                }
            }
        }

        private Dictionary<int, csStudent> dicStudents = new Dictionary<int, csStudent>();

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            int irStudentId = 0;
            Int32.TryParse(txtStudentId.Text, out irStudentId);
            if (irStudentId == 0)
            {
                MessageBox.Show("Error: Please enter a valid integer student Id");
                return;
            }
            if (txtStudentName.Text.Length < 2)
            {
                MessageBox.Show("Error: Please enter a valid student name");
                return;
            }
            if ( txtStudentName.Text[0])
            {
                MessageBox.Show("Error: Please enter a valid student name");
                return;
            }
            csStudent myStudent = new csStudent();
        }
    }
}
