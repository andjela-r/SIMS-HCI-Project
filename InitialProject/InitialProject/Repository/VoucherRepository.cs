using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.View.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    public class VoucherRepository
    {
        private const string FilePath = "../../../Resources/Data/vouchers.csv";

        private readonly Serializer<Voucher> _serializer;

        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(FilePath);
        }

        public List<Voucher> FindAll()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            var result = _vouchers.Where(x => x.ExpirationDate > DateTime.Now).ToList();
            return result;
        }

        public Voucher FindById(int id)
        {
            return _vouchers.Find(x => x.Id == id);
        }

        public List<Voucher> FindByTouristId(int touristId)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            return _vouchers.FindAll(x => x.TouristId == touristId && x.ExpirationDate > DateTime.Now);
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.Id = NextId();
            _vouchers = _serializer.FromCSV(FilePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public int NextId()
        {
            _vouchers = _serializer.FromCSV(FilePath);
            if (_vouchers.Count < 1) return 1;
            return _vouchers.Max(c => c.Id) + 1;
        }

        public Voucher Update(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher current = _vouchers.Find(c => c.Id == voucher.Id);
            int index = _vouchers.IndexOf(current);
            _vouchers.Remove(current);
            _vouchers.Insert(index, voucher);
            _serializer.ToCSV(FilePath, _vouchers);
            return voucher;
        }

        public void Delete(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(FilePath);
            Voucher current = _vouchers.Find(c => c.Id == voucher.Id);
            int index = _vouchers.IndexOf(current);
            _vouchers.Remove(current);
            _serializer.ToCSV(FilePath, _vouchers);
        }
    }
}
