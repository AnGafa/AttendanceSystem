using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GroupRepository : ConnectionClass
    {
        public GroupRepository() : base()
        {
        }

        public IQueryable<Group> GetGroup()
        {
            return MyConnection.Groups;
        }

        public bool DoesGroupExist(int gID)
        { 
            return MyConnection.Groups.Any(g => g.GroupID.Equals(gID));
        }

        public void addNewGroup(string groupName, string courseName)
        {
            Group newGroup = new Group();
            newGroup.Name = groupName;
            newGroup.Course = courseName;

            MyConnection.Groups.Add(newGroup);
            MyConnection.SaveChanges();
        }
    }
}
