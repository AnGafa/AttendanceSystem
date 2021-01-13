using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class LessonService
    {
        private LessonRepository _LessonRepo;
        public LessonService()
        {
            _LessonRepo = new LessonRepository();
        }

        public List<Lesson> GetLessons()
        {
            return _LessonRepo.GetLessons().ToList();
        }

        public int GetNumberLessonsAtDate(int loggedUserID, DateTime userDate)
        {
            List<Lesson> lessons =  _LessonRepo.GetLessons().Where(LessonID => (
                LessonID.TeacherFK == loggedUserID &&
                LessonID.DateTime.Value.Day == userDate.Day && 
                LessonID.DateTime.Value.Month == userDate.Month && 
                LessonID.DateTime.Value.Year == userDate.Year
             )).ToList();



            return lessons.Count;
        }

        public void Add(Lesson l)
        {
            _LessonRepo.Add(l);
        }

        public int  addNewLesson(int groupID, DateTime date, int teacherID)
        {
            return _LessonRepo.addNewLesson(groupID, date, teacherID);
        }
    }
}
