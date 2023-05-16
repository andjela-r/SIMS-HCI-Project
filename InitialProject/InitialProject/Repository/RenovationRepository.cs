using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class RenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";
        private readonly Serializer<Renovation> _serializer;

        private List<Renovation> _renovations;

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = _serializer.FromCSV(FilePath);
        }

        public Renovation Save(Renovation renovation)
        {
            renovation.Id = NextId();
            _renovations = _serializer.FromCSV(FilePath);
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(c => c.Id) + 1;
        }

        public Renovation FindById(int id)
        {
            return _renovations.Find(x => x.Id == id);
        }

        public List<Renovation> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<Renovation> FindByAccommodationId(int accId)
        {
            _renovations = _serializer.FromCSV(FilePath);
            return _renovations.FindAll(u => u.AccommodationId == accId);
        }

        public void Delete(Renovation renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation founded = _renovations.Find(c => c.Id == renovation.Id);
            _renovations.Remove(founded);
            _serializer.ToCSV(FilePath, _renovations);
        }


    }
}
