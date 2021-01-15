using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class TeacherService
    {
        private TeacherRepository _teacherRepo;
        public TeacherService()
        {
            _teacherRepo = new TeacherRepository();
        }

        public List<Teacher> GetTeachers()
        {
            return _teacherRepo.GetTeachers().ToList();
        }

        public bool DoesTeacherNameExist(string uName)
        {
            return _teacherRepo.DoesTeacherNameExist(uName);
        }

        public bool DoesTeacherIDExist(int uID)
        {
            return _teacherRepo.DoesTeacherIDExist(uID);
        }

        public int VerifyCredentials(string userName, string password)
        {
            return _teacherRepo.VerifyCredentials(userName, password);
        }

        public void addNewTeacher(int teacherID, string teacherUsername, string teacherPassword, string teacherName, string teacherSurname, string teacherEmail)
        {
            _teacherRepo.addNewTeacher(teacherID, teacherUsername, teacherPassword, teacherName, teacherSurname, teacherEmail);
        }
    }
}
