using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using nawras_data_access.BookingsData;
namespace nawras_business_layer.Bookings
{
    public static class clsBookingStatus
    {

        public static DataTable ListAllBookingStatuses()
        {
            return clsBookingStatusDataAccess.ListAllBookingStatuses();
        }
    
    }
}
