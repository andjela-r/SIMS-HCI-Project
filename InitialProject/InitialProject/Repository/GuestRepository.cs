using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    internal class GuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guests.csv";

        private readonly Serializer<Guest> _serializer;

        private List<Guest> _guests;

        public GuestRepository()
        {
            _serializer = new Serializer<Guest>();
            _guests = _serializer.FromCSV(FilePath);
        }

        public Guest Save(Guest guest)
        {
            guest.Id = NextId();
            _guests = _serializer.FromCSV(FilePath);
            _guests.Add(guest);
            _serializer.ToCSV(FilePath, _guests);
            return guest;
        }

        public int NextId()
        {
            _guests = _serializer.FromCSV(FilePath);
            if (_guests.Count < 1)
            {
                return 1;
            }
            return _guests.Max(c => c.Id) + 1;
        }

        public List<Guest> FindByName(string name)
        {
            _guests = _serializer.FromCSV(FilePath);
            return _guests.FindAll(u => u.Username == name);
        }

        public Guest FindById(int id)
        {
            return _guests.Find(x => x.Id == id);
        }

        public Guest FindByUserId(int userId)
        {
            return _guests.Find(x => x.UserId == userId);
        }

        public List<Guest> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Guest Update(Guest guest)
        {
            _guests = _serializer.FromCSV(FilePath);
            Guest current = _guests.Find(u => u.Id == guest.Id);
            int index = _guests.IndexOf(current);
            _guests.Remove(current);
            _guests.Insert(index, guest);
            _serializer.ToCSV(FilePath, _guests);
            return guest;
        }
    }
}
