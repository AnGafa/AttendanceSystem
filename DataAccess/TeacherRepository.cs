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

        public bool DoesTeacherExist(string uName)
        {
            return MyConnection.Teachers.Any(t => t.Username.Equals(uName)); 
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
    }
}
