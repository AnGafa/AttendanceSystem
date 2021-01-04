using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace BusinessLogic
{
    public class GroupService
    {
        private GroupRepository _groupRepo;
        public GroupService()
        {
            _groupRepo = new GroupRepository();
        }
 
        public List<Group> GetGroup()
        {
            return _groupRepo.GetGroup().ToList();
        }

        public void addNewGroup(string groupName, string courseName)
        {
            _groupRepo.addNewGroup(groupName, courseName);
        }
        public bool DoesGroupExist(int gID)
        {
            return _groupRepo.DoesGroupExist(gID);
        }
    }
}
