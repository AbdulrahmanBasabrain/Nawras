using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.paymentsData
{
    public static class clsGeneratedBillDataAccess
    {



        public static bool GetGeneratedBillInfoById(int generatedBillId, ref decimal generatedBillFees, ref int billTypeId, ref int bookingId,
        ref DateTime generatedBillDate, ref DateTime createdAt, ref short billPaymentStatusId,
            ref DateTime billExpectedPaymentDate, ref bool isPaid)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from generated_bills where generated_bill_id = @GeneratedBillId;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@GeneratedBillId", generatedBillId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;
                    generatedBillFees = reader.GetDecimal(reader.GetOrdinal("generated_bill_fees"));
                    billTypeId = reader.GetInt32(reader.GetOrdinal("bill_type_id"));
                    bookingId = reader.GetInt32(reader.GetOrdinal("booking_id"));
                    generatedBillDate = reader.GetDateTime(reader.GetOrdinal("generated_bill_date"));
                    createdAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                    billPaymentStatusId = reader.GetInt16(reader.GetOrdinal("bill_payment_status_id"));
                    billExpectedPaymentDate = reader.GetDateTime(reader.GetOrdinal("bill_expected_payment_date"));
                    isPaid = reader.GetBoolean(reader.GetOrdinal("is_paid"));
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

        public static DataTable ListAllGeneratedBills()
        {
            DataTable generatedBillsTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from generated_bills;";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                generatedBillsTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return generatedBillsTable;
        }

        public static int AddNewGeneratedBill(decimal generatedBillFees, int billTypeId,
            int bookingId, DateTime generatedBillDate, DateTime createdAt, short billPaymentStatusId,
            DateTime billExpectedPaymentDate, bool isPaid)
        {
            int generatedBillId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"
INSERT INTO [dbo].[generated_bills]
           ([generated_bill_fees]
           ,[bill_type_id]
           ,[booking_id]
           ,[generated_date]
           ,[bill_payment_status_id]
           ,[expected_payment_date]
           ,[is_paid]
           ,[created_at])
                            values (@generatedBillFees, @BillTypeId, @bookingId, 
                            @generatedBillDate, @createdAt, @billPaymentStatusId, @billExpectedPaymentDate, @isPaid);
                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillFees", generatedBillFees);
            command.Parameters.AddWithValue("@billTypeId", billTypeId);
            command.Parameters.AddWithValue("@bookingId", bookingId);
            command.Parameters.AddWithValue("@generatedBillDate", generatedBillDate);
            command.Parameters.AddWithValue("@createdAt", createdAt);
            command.Parameters.AddWithValue("@billPaymentStatusId", billPaymentStatusId);
            command.Parameters.AddWithValue("@billExpectedPaymentDate", billExpectedPaymentDate);
            command.Parameters.AddWithValue("@isPaid", isPaid);

            try
            {

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    generatedBillId = insertedId;
                }
                else
                {
                    generatedBillId = -1; // In case of failure to insert
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }
            return generatedBillId;

        }

        public static bool UpdateGeneratedBill(int generatedBillId, decimal generatedBillFees, int billTypeId,
            int bookingId, DateTime generatedBillDate, DateTime createdAt, short billPaymentStatusId,
            DateTime billExpectedPaymentDate, bool isPaid)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"
GO

UPDATE [dbo].[generated_bills]
   SET [generated_bill_fees] = @generatedBillFees
      ,[bill_type_id] = @billTypeId
      ,[booking_id] = bookingId
      ,[generated_date] = @generatedBillDate
      ,[created_at] = @createdAt
      ,[bill_payment_status_id] = @billPaymentStatusId
      ,[expected_payment_date] = @billExpectedPaymentDate
      ,[is_paid] = @isPaid
 WHERE generated_bill_id = @generatedBillId
GO


                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);
            command.Parameters.AddWithValue("@generatedBillFees", generatedBillFees);
            command.Parameters.AddWithValue("@billTypeId", billTypeId);
            command.Parameters.AddWithValue("@bookingId", bookingId);
            command.Parameters.AddWithValue("@generatedBillDate", generatedBillDate);
            command.Parameters.AddWithValue("@createdAt", createdAt);
            command.Parameters.AddWithValue("@billPaymentStatusId", billPaymentStatusId);
            command.Parameters.AddWithValue("@billExpectedPaymentDate", billExpectedPaymentDate);
            command.Parameters.AddWithValue("@isPaid", isPaid);

            try
            {

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }
            return (rowsAffected > 0);

        }

        public static bool DeleteGeneratedBill(int generatedBillId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"
GO

DELETE FROM [dbo].[generated_bills]
 WHERE generated_bill_id = @generatedBillId
GO

                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);


            try
            {

                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();

            }
            return (rowsAffected > 0);


        }

        public static bool IsGeneratedBillExist(int generatedBillId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query =

                @"
GO

select * FROM [dbo].[generated_bills]
 WHERE generated_bill_id = @generatedBillId
GO

                ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@generatedBillId", generatedBillId);


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
