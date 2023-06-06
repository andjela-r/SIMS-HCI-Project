using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class GuideRepository
    {
        private const string FilePath = "../../../Resources/Data/guides.csv";

        private readonly Serializer<Guide> _serializer;

        private List<Guide> _guides;

        public GuideRepository()
        {
            _serializer = new Serializer<Guide>();
            _guides = _serializer.FromCSV(FilePath);
        }

        public List<Guide> FindAll()
        {
            _guides = _serializer.FromCSV(FilePath);
            return _guides;
        }

        public Guide FindById(int guideId)
        {
            _guides= _serializer.FromCSV(FilePath);
            return _guides.Find(x => x.UserId == guideId);
        }

        public Guide Update(Guide guide)
        {
            _guides = _serializer.FromCSV(FilePath);
            Guide current = _guides.Find(c => c.UserId == guide.UserId);
            int index = _guides.IndexOf(current);
            _guides.Remove(current);
            _guides.Insert(index, guide);
            _serializer.ToCSV(FilePath, _guides);
            return guide;
        }

        public void ChangeResignationStatus(int id)
        {
            Guide guide = _guides.Find(x => x.UserId == id);
            guide.Resignation = true;
            Update(guide);
        }
    }
}
