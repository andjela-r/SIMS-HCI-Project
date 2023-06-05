using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Tourist : User
    { 
        //public List<Voucher> Vouchers { get; set; }
        public int NumOfToursThisYear { get; set; }
        public bool GotYearlyVoucherReward { get; set; }

        public Tourist() { }

        public Tourist(string username, string password, Role role, int numOfToursThisYear = 0, bool gotVoucher = false) : base(username, password, role)
        {
            this.NumOfToursThisYear = numOfToursThisYear;
            this.GotYearlyVoucherReward = gotVoucher;
        }
    }
}
