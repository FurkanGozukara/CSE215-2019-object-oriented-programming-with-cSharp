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
        //call this whenever modify dictionary student
        public static void saveStudentsPrimitive(string srStudentSaveFileName, Dictionary<int, csStudent> dicStudents, char crStudentRecordSplit, char crLessonRecordSplit, char crLessonListSplit)
        {
            StreamWriter swWriteStudents = new StreamWriter(srStudentSaveFileName);
            foreach (var vrStudent in dicStudents)
            {
                string srLesson = composeLessons(vrStudent.Value.lstLessons, crLessonListSplit, crLessonListSplit);
                string srStudentRecord = string.Format("{0}{1}{2}{1}{3}",
                    vrStudent.Value.irStudentId, crStudentRecordSplit, vrStudent.Value.srStudentName, srLesson);
                swWriteStudents.WriteLine(srStudentRecord);
            }
            swWriteStudents.Flush();
            swWriteStudents.Close();
        }

        private static string composeLessons(List<csLesson> lstLessons, char crLessonRecordSplit, char crLessonListSplit)
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
    }
}
