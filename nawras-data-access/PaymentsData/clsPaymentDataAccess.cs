using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.paymentsData
{
    public static class clsPaymentDataAccess
    {


        public static bool GetPaymentInfoById(int paymentId, ref decimal paymentAmount, ref int paidByTourisId, ref int generatedBillId,
         ref int recordedBySystemUserId, ref int receivedByOwnerId, ref DateTime paymentDate, ref short paymentMethodId,
         ref short paymentStatusId, ref DateTime createdAt)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from payments where payment_id = @paymentId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentId", paymentId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    paymentAmount = reader.GetDecimal(reader.GetOrdinal("payment_amount"));
                    paidByTourisId = reader.GetInt32(reader.GetOrdinal("paid_by_tourist_id"));
                    generatedBillId = reader.GetInt32(reader.GetOrdinal("generated_bill_id"));
                    recordedBySystemUserId = reader.GetInt32(reader.GetOrdinal("recorded_by_system_user_id"));
                    receivedByOwnerId = reader.GetInt32(reader.GetOrdinal("received_by_owner_id"));
                    paymentDate = reader.GetDateTime(reader.GetOrdinal("payment_date"));
                    paymentMethodId = reader.GetInt16(reader.GetOrdinal("payment_method_id"));
                    paymentStatusId = reader.GetInt16(reader.GetOrdinal("payment_status_id"));
                    createdAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                }
                else
                {
                    isFound = false;
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();

            }

            return isFound;
        }

        public static DataTable ListAllPayments()
        {
            DataTable paymentsTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from payments;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                paymentsTable.Load(reader);
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
            return paymentsTable;
        }

        public static int AddPayment(decimal paymentAmount, int paidByTourisId, int generatedBillId, int recordedBySystemUserId, int receivedByOwnerId,
            DateTime paymentDate, short paymentMethodId, short paymentStatusId, DateTime createdAt)
        {
            int paymentId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                insert into payments (payment_amount, paid_by_tenant_id, generated_bill_id, recorded_by_system_user_id, received_by_owner_id,
                payment_date, payment_method_id, payment_status_id, created_at)
                output inserted.payment_id
                values (@paymentAmount, @paidByTenantId, @generatedBillId, @recordedBySystemUserId, @receivedByOwnerId,
                @paymentDate, @paymentMethodId, @paymentStatusId, @createdAt);";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentAmount", paymentAmount);
            command.Parameters.AddWithValue("@paidByTenantId", paidByTourisId);
            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);
            command.Parameters.AddWithValue("@recordedBySystemUserId", recordedBySystemUserId);
            command.Parameters.AddWithValue("@receivedByOwnerId", receivedByOwnerId);
            command.Parameters.AddWithValue("@paymentDate", paymentDate);
            command.Parameters.AddWithValue("@paymentMethodId", paymentMethodId);
            command.Parameters.AddWithValue("@paymentStatusId", paymentStatusId);
            command.Parameters.AddWithValue("@createdAt", createdAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    paymentId = insertedId;
                }

            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }
            return paymentId;
        }

        public static bool UpdatePayment(int paymentId, decimal paymentAmount, int paidByTourisId, int generatedBillId, int recordedBySystemUserId, int receivedByOwnerId,
            DateTime paymentDate, short paymentMethodId, short paymentStatusId, DateTime createdAt)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                update payments
                set payment_amount = @paymentAmount,
                    paid_by_tenant_id = @paidByTourisId,
                    generated_bill_id = @generatedBillId,
                    recorded_by_system_user_id = @recordedBySystemUserId,
                    received_by_owner_id = @receivedByOwnerId,
                    payment_date = @paymentDate,
                    payment_method_id = @paymentMethodId,
                    payment_status_id = @paymentStatusId,
                    created_at = @createdAt
                where payment_id = @paymentId;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentId", paymentId);
            command.Parameters.AddWithValue("@paymentAmount", paymentAmount);
            command.Parameters.AddWithValue("@paidByTenantId", paidByTourisId);
            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);
            command.Parameters.AddWithValue("@recordedBySystemUserId", recordedBySystemUserId);
            command.Parameters.AddWithValue("@receivedByOwnerId", receivedByOwnerId);
            command.Parameters.AddWithValue("@paymentDate", paymentDate);
            command.Parameters.AddWithValue("@paymentMethodId", paymentMethodId);
            command.Parameters.AddWithValue("@paymentStatusId", paymentStatusId);
            command.Parameters.AddWithValue("@createdAt", createdAt);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool DeletePayment(int paymentId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

                DELETE FROM [dbo].[payments]

                where payment_id = @paymentId;
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentId", paymentId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsPaymentExist(int paymentId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"
GO

select * FROM [dbo].[payments]
                where payment_id = @paymentId;
GO

                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@paymentId", paymentId);


            try
            {

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }

            return isFound;


        }



















    }
}
