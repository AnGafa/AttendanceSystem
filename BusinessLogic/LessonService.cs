using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
