using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.paymentsData;

namespace nawras_business_layer.payments
{

    public static class clsPaymentStatus
    {

        public static DataTable ListAllPaymentStatuses()
        {

            return clsPaymentStatusDataAccess.ListAllPaymentStatuses();

        }
    }

}
