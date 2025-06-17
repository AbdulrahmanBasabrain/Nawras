using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.paymentsData;

namespace nawras_business_layer.payments
{
    public class clsPayment
    {

        private enum enMode { enAddPayment = 1, enUpdatePayment = 2 }
        private enMode _Mode = enMode.enAddPayment;

        public int PaymentId { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaidByTouristId { get; set; }
        public int GeneratedBillId { get; set; }
        public int RecordedBySystemUserId { get; set; }
        public int ReceivedByOwnerId { get; set; }
        public DateTime PaymentDate { get; set; }
        public short PaymentMethodId { get; set; }
        public short PaymentStatusId { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsPayment()
        {
            PaymentId = -1;
            PaymentAmount = 0.0m;
            PaidByTouristId = -1;
            GeneratedBillId = -1;
            RecordedBySystemUserId = -1;
            ReceivedByOwnerId = -1;
            PaymentDate = DateTime.MinValue;
            PaymentMethodId = -1;
            PaymentStatusId = -1;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddPayment;
        }

        public clsPayment(int paymentId, decimal paymentAmount, int paidByTourisId, int generatedBillId, int recordedBySystemUserId, int receivedByOwnerId,
            DateTime paymentDate, short paymentMethodId, short paymentStatusId, DateTime createdAt)
        {
            PaymentId = paymentId;
            PaymentAmount = paymentAmount;
            PaidByTouristId = paidByTourisId;
            GeneratedBillId = generatedBillId;
            RecordedBySystemUserId = recordedBySystemUserId;
            ReceivedByOwnerId = receivedByOwnerId;
            PaymentDate = paymentDate;
            PaymentMethodId = paymentMethodId;
            PaymentStatusId = paymentStatusId;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdatePayment;
        }


        public static clsPayment FindPaymentById(int paymentId)
        {

            decimal paymentAmount = 0.0m;
            int paidByTourisId = -1;
            int generatedBillId = -1;
            int recordedBySystemUserId = -1;
            int receivedByOwnerId = -1;
            DateTime paymentDate = DateTime.MinValue;
            short paymentMethodId = -1;
            short paymentStatusId = -1;
            DateTime createdAt = DateTime.Now;

            if (clsPaymentDataAccess.GetPaymentInfoById(paymentId, ref paymentAmount, ref paidByTourisId, ref generatedBillId,
                ref recordedBySystemUserId, ref receivedByOwnerId, ref paymentDate,
                ref paymentMethodId, ref paymentStatusId, ref createdAt))
            {
                return new clsPayment(paymentId, paymentAmount, paidByTourisId, generatedBillId,
                    recordedBySystemUserId, receivedByOwnerId, paymentDate, paymentMethodId,
                    paymentStatusId, createdAt);
            }
            else
            {
                return null;
            }

        }

        public static DataTable ListAllPayments()
        {
            return clsPaymentDataAccess.ListAllPayments();
        }

        private bool _AddNewPayment()
        {

            this.PaymentId = clsPaymentDataAccess.AddPayment(
                this.PaymentAmount,
                this.PaidByTouristId,
                this.GeneratedBillId,
                this.RecordedBySystemUserId,
                this.ReceivedByOwnerId,
                this.PaymentDate,
                this.PaymentMethodId,
                this.PaymentStatusId,
                this.CreatedAt);

            return (this.PaymentId != -1);

        }

        private bool _UpdatePayment()
        {

            return clsPaymentDataAccess.UpdatePayment(
                this.PaymentId,
                this.PaymentAmount,
                this.PaidByTouristId,
                this.GeneratedBillId,
                this.RecordedBySystemUserId,
                this.ReceivedByOwnerId,
                this.PaymentDate,
                this.PaymentMethodId,
                this.PaymentStatusId,
                this.CreatedAt);

        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddPayment:
                    if (_AddNewPayment())
                    {
                        _Mode = enMode.enUpdatePayment;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdatePayment:
                    return _UpdatePayment();
            }

            return false;
        }

        public static bool Delete(int paymentId)
        {
            return clsPaymentDataAccess.DeletePayment(paymentId);
        }

        public static bool IsPaymentExist(int paymentId)
        {

            return clsPaymentDataAccess.IsPaymentExist(paymentId);

        }

    }
}
