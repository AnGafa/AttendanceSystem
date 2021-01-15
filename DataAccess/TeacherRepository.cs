using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class TeacherRepository : ConnectionClass
    {
        public TeacherRepository() : base()
        {
        }

        public IQueryable<Teacher> GetTeachers()
        {
            return MyConnection.Teachers;
        }

        public bool DoesTeacherNameExist(string uName)
        {
            return MyConnection.Teachers.Any(t => t.Username.Equals(uName)); 
        }

        public bool DoesTeacherIDExist(int uID)
        {
            return MyConnection.Teachers.Any(t => t.TeacherID.Equals(uID));
        }

        public int VerifyCredentials(string userName, string password)
        {
            if (MyConnection.Teachers.Any(t => t.Username.Equals(userName) && t.Password.Equals(password)))
            {
                var query = MyConnection.Teachers.Where(th => th.Username.Equals(userName) && th.Password.Equals(password));

                Teacher teacher = query.FirstOrDefault<Teacher>();


                return teacher.TeacherID;
            }
            else
                return 0;
        }
        public void addNewTeacher(int teacherID, string teacherUsername, string teacherPassword, string teacherName, string teacherSurname, string teacherEmail)
        {
            Teacher newTeacher = new Teacher();
            newTeacher.TeacherID = teacherID;
            newTeacher.Username = teacherUsername;
            newTeacher.Password = teacherPassword;
            newTeacher.Name = teacherName;
            newTeacher.Surname = teacherSurname;
            newTeacher.Email = teacherEmail;

            MyConnection.Teachers.Add(newTeacher);
            MyConnection.SaveChanges();
        }

    }
}
