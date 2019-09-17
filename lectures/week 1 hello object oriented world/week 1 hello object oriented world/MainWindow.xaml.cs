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
            loadStudentsPrimitive();
        }

        public class csStudent
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
            public List<csLesson> lstLessons = new List<csLesson>();
        }

        public class csLesson
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
            if (txtStudentName.Text.Any(pr => pr.isInteger()) == true)
            {
                MessageBox.Show("Error: Please enter a valid student name");
                return;
            }
            csStudent myStudent = new csStudent(_StudentName: txtStudentName.Text);

            csStudent myStudentCopy = new csStudent();

            myStudentCopy = csCustomExtensions.copyCsStudent(myStudent);

            myStudentCopy = copyCsStudent(myStudent);

            myStudentCopy = myStudent.copyCsStudent();

            myStudentCopy.srStudentName = "test";

            try
            {
                myStudent.irStudentId = txtStudentId.Text.myCustomToInt();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }

            if (dicStudents.ContainsKey(myStudent.irStudentId))
            {
                MessageBox.Show("Error: This student Id already exists");
                return;
            }

            dicStudents.Add(myStudent.irStudentId, myStudent);
            saveStudentsPrimitive();
            refreshListBox();
        }

        private void refreshListBox()
        {
            lstStudentsList.Items.Clear();

            dicStudents = dicStudents.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            foreach (var vrPerStudent in dicStudents.Values)
            {
                var vrValue = string.Format("Id: {0}\t{1}", vrPerStudent.irStudentId.ToString("N0"), vrPerStudent.srStudentName);
                lstStudentsList.Items.Add(vrValue);
            }

            //sort dictionary by student id first
            //then add student information to the listbox
        }


        public csStudent copyCsStudent(csStudent myStudent)
        {
            csStudent tempStudent = new csStudent();
            tempStudent.srStudentName = myStudent.srStudentName;
            tempStudent.irStudentId = myStudent.irStudentId;

            //this equals to above 3 line
            return new csStudent(myStudent.irStudentId, myStudent.srStudentName);

            return tempStudent;
        }

        private string srStudentSaveFileName = "student_records.txt";
        private char crStudentRecordSplit = '$';
        private char crLessonRecordSplit = '#';
        private char crLessonListSplit = '|';

        //call this whenever modify dictionary student
        private void saveStudentsPrimitive()
        {
            StreamWriter swWriteStudents = new StreamWriter(srStudentSaveFileName);
            foreach (var vrStudent in dicStudents)
            {
                string srLesson = composeLessons(vrStudent.Value.lstLessons);
                string srStudentRecord = string.Format("{0}{1}{2}{1}{3}",
                    vrStudent.Value.irStudentId, crStudentRecordSplit, vrStudent.Value.srStudentName, srLesson);
                swWriteStudents.WriteLine(srStudentRecord);
            }
            swWriteStudents.Flush();
            swWriteStudents.Close();
        }

        private string composeLessons(List<csLesson> lstLessons)
        {
            List<string> lstRecords = new List<string>();

            foreach (var vrLeson in lstLessons)
            {
                string srLessonRecord = string.Format("{0}{1}{2}{1}{3}", 
                    vrLeson.irLessonId, // index 0
                    crLessonRecordSplit, 
                    vrLeson.srLessonName, // index 1
                    crLessonRecordSplit, 
                    vrLeson.irFinalScore); // index 2

                lstRecords.Add(srLessonRecord);
            }

            return string.Join(crLessonListSplit.ToString(), lstRecords);
        }

        //call this when the application starts
        private void loadStudentsPrimitive()
        {
            foreach (var vrLine in File.ReadLines(srStudentSaveFileName))
            {
                csStudent studentTemp = new csStudent();
                var vrSplitted = vrLine.Split(crStudentRecordSplit);
                studentTemp.irStudentId = Convert.ToInt32(vrSplitted[0]);
                studentTemp.srStudentName = vrSplitted[1];
                studentTemp.lstLessons= composeLessonsFromText(vrSplitted[2]);
                dicStudents.Add(studentTemp.irStudentId, studentTemp);
            }

            refreshListBox();
        }

        private List<csLesson> composeLessonsFromText(string srLine)
        {
            List<csLesson> lstListLessons = new List<csLesson>();

            foreach (var vrPerLesson in srLine.Split(crLessonListSplit))
            {
                if (string.IsNullOrEmpty(vrPerLesson))
                    continue;

                csLesson myTempLesson = new csLesson();
                var vrSplitLesson = vrPerLesson.Split(crLessonRecordSplit);
                myTempLesson.irLessonId= Convert.ToInt32(vrSplitLesson[0]);
                myTempLesson.srLessonName = vrSplitLesson[1];
                myTempLesson.irFinalScore = Convert.ToInt32(vrSplitLesson[2]);
                lstListLessons.Add(myTempLesson);
            }

            return lstListLessons;
        }
    }
}
