using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.paymentsData;

namespace nawras_business_layer.payments
{
    public class clsGeneratedBill
    {

        private enum enMode { enAddGeneratedBill = 1, enUpdateGeneratedBill = 2 }

        private enMode _Mode = enMode.enAddGeneratedBill;

        public int GeneratedBillId { get; set; }
        public decimal GeneratedBillFees { get; set; }
        public int BillTypeId { get; set; }
        public int BookingId { get; set; }
        public DateTime GeneratedBillDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public short BillPaymentStatusId { get; set; }
        public DateTime BillExpectedPaymentDate { get; set; }
        public bool IsPaid { get; set; }

        public clsGeneratedBill()
        {
            GeneratedBillId = -1;
            GeneratedBillFees = 0.0m;
            BillTypeId = -1;
            BookingId = -1;
            GeneratedBillDate = DateTime.MinValue;
            CreatedAt = DateTime.Now;
            BillPaymentStatusId = -1;
            BillExpectedPaymentDate = DateTime.MinValue;
            IsPaid = false;

            _Mode = enMode.enAddGeneratedBill;

        }

        private clsGeneratedBill(int generatedBillId, decimal generatedBillFees, int billTypeId, int bookingId, DateTime generatedBillDate, DateTime createdAt, short billPaymentStatusId,
            DateTime billExpectedPaymentDate, bool isPaid)
        {

            GeneratedBillId = generatedBillId;
            GeneratedBillFees = generatedBillFees;
            BillTypeId = billTypeId;
            BookingId = bookingId;
            GeneratedBillDate = generatedBillDate;
            CreatedAt = createdAt;
            BillPaymentStatusId = billPaymentStatusId;
            BillExpectedPaymentDate = billExpectedPaymentDate;
            IsPaid = isPaid;


            _Mode = enMode.enUpdateGeneratedBill;

        }

        public static clsGeneratedBill FindGeneratedBillById(int generatedBillId)
        {

            decimal generatedBillFees = 0.0m;
            int billTypeId = -1;
            int bookingId = -1;
            DateTime generatedBillDate = DateTime.MinValue;
            DateTime createdAt = DateTime.Now;
            short billPaymentStatusId = -1;
            DateTime billExpectedPaymentDate = DateTime.MinValue;
            bool isPaid = false;

            if (clsGeneratedBillDataAccess.GetGeneratedBillInfoById(generatedBillId, ref generatedBillFees, ref billTypeId, ref bookingId, ref generatedBillDate,
                ref createdAt, ref billPaymentStatusId, ref billExpectedPaymentDate, ref isPaid))
            {
                return new clsGeneratedBill(generatedBillId, generatedBillFees, billTypeId, bookingId, generatedBillDate, createdAt, billPaymentStatusId, billExpectedPaymentDate, isPaid);
            }
            else
            {
                return null;
            }

        }

        public static DataTable ListAllGeneratedBills()
        {
            return clsGeneratedBillDataAccess.ListAllGeneratedBills();
        }

        private bool _AddNewGeneratedBill()
        {
            this.GeneratedBillId = clsGeneratedBillDataAccess.AddNewGeneratedBill(this.GeneratedBillFees, this.BillTypeId,
                this.BookingId, this.GeneratedBillDate, this.CreatedAt, this.BillPaymentStatusId,
                this.BillExpectedPaymentDate, this.IsPaid);

            return (this.GeneratedBillId != -1);
        }

        private bool _UpdateGeneratedBill()
        {
            return clsGeneratedBillDataAccess.UpdateGeneratedBill(this.GeneratedBillId, this.GeneratedBillFees, this.BillTypeId,
                this.BookingId, this.GeneratedBillDate, this.CreatedAt, this.BillPaymentStatusId,
                this.BillExpectedPaymentDate, this.IsPaid);
        }

        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.enAddGeneratedBill:
                    if (_AddNewGeneratedBill())
                    {
                        _Mode = enMode.enUpdateGeneratedBill;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.enUpdateGeneratedBill:
                    return _UpdateGeneratedBill();



            }

            return false;

        }

        public bool Delete()
        {
            return clsGeneratedBillDataAccess.DeleteGeneratedBill(this.GeneratedBillId);
        }

        public bool IsGeneratedBillExist()
        {
            return clsGeneratedBillDataAccess.IsGeneratedBillExist(this.GeneratedBillId);
        }


    }

}
