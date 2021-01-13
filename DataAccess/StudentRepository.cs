using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StudentRepository : ConnectionClass
    {
        public StudentRepository() : base()
        {
        }

        public IQueryable<Student> GetStudents()
        {
            return MyConnection.Students;
        }

        public bool DoesStudentExist(int sID)
        {
            return MyConnection.Students.Any(s => s.StudentID.Equals(sID));
        }

        public void addNewStudent(int studentID, string studentName, string studentSurname, string studentEmail, int studentGroupID)
        {
            Student newStudent = new Student();
            newStudent.StudentID = studentID;
            newStudent.Name = studentName;
            newStudent.Surname = studentSurname;
            newStudent.Email = studentEmail;
            newStudent.GroupFK = studentGroupID;

            MyConnection.Students.Add(newStudent);
            MyConnection.SaveChanges();
        }
    }
}
