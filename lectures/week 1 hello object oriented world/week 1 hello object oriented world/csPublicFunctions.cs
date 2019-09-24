using nmStudent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week_1_hello_object_oriented_world
{
    public static class csPublicFunctions
    {
        public static class constantVariables
        {
    //A field is a variable that is declared directly in a class or struct. ... A property is a member that provides a flexible mechanism to read, write, or compute the value of a private field.Properties can be used as if they are public data members, but they are actually special methods called accessors. In object oriented programming, property usage is preffered

            public const string srStudentSaveFileName = "student_records.txt";//field
            public static char crStudentRecordSplit { get; set; } = '$';//auto property

            private static char _crLessonRecordSplit;
           
            public static char crLessonRecordSplit //property full
            {
                get
                {
                    return _crLessonRecordSplit;
                }
                set
                {
                    _crLessonRecordSplit = value;
                }
            }

            static constantVariables()
            {
                crLessonRecordSplit = '#';
            }

            private static char _crLessonListSplit= '|';
            //this can only be read
            public static char crLessonListSplit
            {
                get
                {
                    return _crLessonRecordSplit;
                }
            }
        }

        //call this whenever modify dictionary student
        public static void saveStudentsPrimitive(Dictionary<int, csStudent> dicStudents)
        {
            StreamWriter swWriteStudents = new StreamWriter(constantVariables.srStudentSaveFileName);
            foreach (var vrStudent in dicStudents)
            {
                string srLesson = composeLessons(vrStudent.Value.lstLessons);
                string srStudentRecord = string.Format("{0}{1}{2}{1}{3}",
                    vrStudent.Value.irStudentId, constantVariables.crStudentRecordSplit, vrStudent.Value.srStudentName, srLesson);
                swWriteStudents.WriteLine(srStudentRecord);
            }
            swWriteStudents.Flush();
            swWriteStudents.Close();
        }

        private static string composeLessons(List<csLesson> lstLessons)
        {
            List<string> lstRecords = new List<string>();

            foreach (var vrLeson in lstLessons)
            {
                string srLessonRecord = string.Format("{0}{1}{2}{1}{3}",
                    vrLeson.irLessonId, // index 0
                    constantVariables.crLessonRecordSplit,
                    vrLeson.srLessonName, // index 1
                    constantVariables.crLessonRecordSplit,
                    vrLeson.irFinalScore); // index 2

                lstRecords.Add(srLessonRecord);
            }

            return string.Join(constantVariables.crLessonListSplit.ToString(), lstRecords);
        }
    }
}
