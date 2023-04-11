using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.DTO;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service
{
    public class VoucherService
    {
        public void CreateVoucher(VoucherDTO voucherDto)
        {
            Voucher voucher = new Voucher();

            voucher.Name = voucherDto.Name;
            voucher.TouristId = voucherDto.TouristId;
            voucher.ExpirationDate = voucherDto.ExpirationDate;

            VoucherRepository voucherRepository = new VoucherRepository();
            voucherRepository.Save(voucher);
        }
    }
}
