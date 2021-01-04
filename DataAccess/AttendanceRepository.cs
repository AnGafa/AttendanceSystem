using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class StudentAttendanceRepository: ConnectionClass
    {
        public StudentAttendanceRepository() : base()
        { 
        }

        public void Add(StudentAttendance a)
        {
            MyConnection.StudentAttendances.Add(a);
            MyConnection.SaveChanges();
        }

        public IQueryable<StudentAttendance> GetStudentAttendances()
        {
            return MyConnection.StudentAttendances;
        }

        public void AddStudentAttandanceRecord(int lessonID, int studentID, bool attandance)
        {
            StudentAttendance newAttandance = new StudentAttendance();
            newAttandance.LessonFK = lessonID;
            newAttandance.StudentFK = studentID;
            newAttandance.Presence = attandance;

            MyConnection.StudentAttendances.Add(newAttandance);
            MyConnection.SaveChanges();
        }
    }
}
