using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access;
using nawras_data_access.TripsData;

namespace nawras_business_layer.Trips
{
    public static class clsTripsStatus
    {




        public static DataTable ListAllTripsStatuses()
        {

            return clsTripStatusDataAccess.ListAllTripsStatuses();
        }


    }
}
