using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class StudentService
    {
        private StudentRepository _studentRepo;

        public StudentService()
        {
            _studentRepo = new StudentRepository();
        }

        public List<Student> GetStudents()
        {
            return _studentRepo.GetStudents().ToList();
        }

        public List<Student> GetStudentsFromGroup(int userGroupID)
        {
            return _studentRepo.GetStudents().Where(Name => Name.GroupFK == userGroupID).ToList();

        }

        public void addNewStudent(int studentID, string studentName, string studentSurname, string studentEmail,int studentGroupID)
        {
            _studentRepo.addNewStudent(studentID, studentName, studentSurname, studentEmail, studentGroupID);
        }

        public bool DoesStudentExist(int sID)
        {
            return _studentRepo.DoesStudentExist(sID);
        }

        public void EditStudent(int studentID, string studentName, string studentSurname, string studentEmail)
        {
            _studentRepo.editStudent(studentID, studentName, studentSurname, studentEmail);
        }
    }
}
