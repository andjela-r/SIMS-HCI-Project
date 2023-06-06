using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

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


        public Forum Update(Forum forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forum current = _forums.Find(u => u.Id == forum.Id);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }

        public Forum FindById(int id)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.Find(u => u.Id == id);
        }

        public List<Forum> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        //???
        public List<Forum> FindByLocationId(int locationId)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.FindAll(u => u.LocationIntId <= locationId);
        }

        public bool IsThereLocationId(int locationId)
        {
            string line;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split('|');
                    _forums = _serializer.FromCSV(FilePath);
                    if (columns.Length > 0 && int.TryParse(columns[1], out int locationIntId) && locationIntId == locationId)
                        return true;
                }
            }
            return false;
        }
       
        public bool IsThereGuestId(int guestId)
        {
            string line;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split('|');
                    _forums = _serializer.FromCSV(FilePath);
                    if (columns.Length > 0 && int.TryParse(columns[2], out int gueststId) && gueststId == guestId)
                        return true;
                }
            }
            return false;
        }

        public Forum FindByGuestId(int guestId)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.Find(u => u.GuestId <= guestId);
        }

        public bool IsForumOpen(Forum forum)
        {
            string line;
            using (StreamReader reader = new StreamReader(FilePath))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split('|');
                    _forums = _serializer.FromCSV(FilePath);
                    if (columns.Length > 0 && bool.TryParse(columns[3], out bool gueststId) && gueststId == forum.IsOpen)
                        return true;
                }
            }
            return false;
        }

    }
    }
