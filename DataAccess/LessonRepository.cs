using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DataAccess
{
    public class LessonRepository : ConnectionClass
    {
        public LessonRepository() : base()
        {
        }

        public IQueryable<Lesson> GetLessons()
        {
            return MyConnection.Lessons;
        }

        public void Add(Lesson l)
        {
            MyConnection.Lessons.Add(l);
            MyConnection.SaveChanges();
        }

        public int addNewLesson(int groupID, DateTime date, int teacherID)
        {
            Lesson newLesson = new Lesson();
            newLesson.GroupFK = groupID;
            newLesson.DateTime = date;
            newLesson.TeacherFK = teacherID;

            newLesson =  MyConnection.Lessons.Add(newLesson);
            MyConnection.SaveChanges();

            return newLesson.LessonID;
        }
    }
}
