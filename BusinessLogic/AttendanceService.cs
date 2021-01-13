using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BusinessLogic
{
    public class AttendanceService
    {
        private StudentAttendanceRepository _attendanceRepo;
        public AttendanceService()
        {
            _attendanceRepo = new StudentAttendanceRepository();
        }

        public void Add(StudentAttendance a)
        {
            if(_attendanceRepo.GetStudentAttendances().SingleOrDefault(x=>x.LessonFK == a.LessonFK) == null)
            {
                _attendanceRepo.Add(a);
            }
            else
            {
                Console.WriteLine("record already exists");
            }
            
        }

        public List<StudentAttendance> GetStudentAttendance()
        {
            return _attendanceRepo.GetStudentAttendances().ToList();
        }

        public List<StudentAttendance> GetStudentAttendances(int StudentID)
        {
            return _attendanceRepo.GetStudentAttendances().Where(attendance => attendance.StudentFK == StudentID).ToList();
        }

        public void SetStudentAttandanceRecord(int lessonID, int studentID, string studentAttendance)
        {
            bool attandance = studentAttendance.ToUpper().Equals("P");

            this._attendanceRepo.AddStudentAttandanceRecord(lessonID, studentID, attandance);
        }

        //public int GetNumberOfAttendances(DateTime userDateTime)
        //{
        //    LessonService lessonService = new LessonService();


        //    List<Lesson> lessons = lessonService.GetLessonsAtDate(userDateTime);

        //    Console.WriteLine("length" + lessons.Count);

        //    foreach (var les in lessons)
        //    {
        //        Console.WriteLine(les.LessonID);
        //    }
        //    return 0;
        //}
    }
}
