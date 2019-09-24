using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmStudent2
{
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
}
