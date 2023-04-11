using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Type = InitialProject.Model.Type;


namespace InitialProject.Repository
{
    public class AccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accs.csv";
        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(c => c.Id) + 1;
        }

        public List<Accommodation> FindByName(string name)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(u => u.Name == name);
        }

        public List<Accommodation> FindByType(Type type)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(u => u.Type == type);
        }

        public List<Accommodation> FindByMaxGuests(int maxGuests)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(u => u.MaxGuests >= maxGuests);
        }

        public List<Accommodation> FindByMinStay(int minStay)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(u => u.MinStay <= minStay);
        }

        public List<Accommodation> FindByLocation(int locationId)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(u => u.LocationId == locationId);
        }

        public Accommodation FindById(int id)
        {
            return _accommodations.Find(x => x.Id == id);
        }

        public List<Accommodation> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }

       /* public string FindNameFromId(int id)
        {
            string name = null;

            try
            {
                using (var reader = new StreamReader(FilePath))
                {
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length >= 2)
                        {
                            int currentId;
                            if (int.TryParse(values[0], out currentId))
                            {
                                if (currentId == id)
                                {
                                    name = values[1];
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as appropriate for your application
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }

            return name;
        }*/
    }
}