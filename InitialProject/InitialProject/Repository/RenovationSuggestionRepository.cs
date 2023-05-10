using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class RenovationSuggestionRepository
    {
        private const string FilePath = "../../../Resources/Data/renovationSuggestions.csv";
        private readonly Serializer<RenovationSuggestion> _serializer;
        private List<RenovationSuggestion> _renovationSuggestions;

        public RenovationSuggestionRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _renovationSuggestions = _serializer.FromCSV(FilePath);
        }

        public RenovationSuggestion Save(RenovationSuggestion renovationSuggestion)
        {
            renovationSuggestion.Id = NextId();
            _renovationSuggestions = _serializer.FromCSV(FilePath);
            _renovationSuggestions.Add(renovationSuggestion);
            _serializer.ToCSV(FilePath, _renovationSuggestions);
            return renovationSuggestion;
        }

        public int NextId()
        {
            _renovationSuggestions = _serializer.FromCSV(FilePath);
            if (_renovationSuggestions.Count < 1)
            {
                return 1;
            }
            return _renovationSuggestions.Max(c => c.Id) + 1;
        }

        public List<RenovationSuggestion> FindAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
