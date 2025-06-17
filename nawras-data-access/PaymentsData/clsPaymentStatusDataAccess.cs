using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.paymentsData
{
    public static class clsPaymentStatusDataAccess
    {








        public static DataTable ListAllPaymentStatuses()
        {

            DataTable PaymentStatusesTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM payment_statuses";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                PaymentStatusesTable.Load(reader);
                reader.Close();

            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }
            return PaymentStatusesTable;

        }
















    }
}
