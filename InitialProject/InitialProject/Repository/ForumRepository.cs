using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ForumRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";

        private readonly Serializer<Forum> _serializer;

        private List<Forum> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forum>();
            _forums = _serializer.FromCSV(FilePath);
        }

        public Forum Save(Forum forum)
        {
            forum.Id = NextId();
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }

        public int NextId()
        {
            _forums = _serializer.FromCSV(FilePath);
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(c => c.Id) + 1;
        }

        public List<Forum> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        //???
        public Forum FindByLocationId(int locationId)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.Find(u => u.LocationIntId <= locationId);
        }
    }
}
