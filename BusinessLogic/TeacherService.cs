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
        private TeacherRepository _TeacherRepo;
        public TeacherService()
        {
            _TeacherRepo = new TeacherRepository();
        }

        public List<Teacher> GetTeachers()
        {
            return _TeacherRepo.GetTeachers().ToList();
        }

        public bool DoesTeacherExist(string uName)
        {
            return _TeacherRepo.DoesTeacherExist(uName);
        }

        public int VerifyCredentials(string userName, string password)
        {
            return _TeacherRepo.VerifyCredentials(userName, password);
        }
    }
}
