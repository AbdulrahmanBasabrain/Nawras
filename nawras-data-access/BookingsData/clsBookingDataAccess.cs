using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.BookingsData
{
    public static class clsBookingDataAccess
    {





        public static bool GetBookingInfoById(int bookingId, ref int bookingPrice, ref int touristId, ref int tripId, ref short billTypeId, ref short bookingStatusId, ref DateTime createdAt)
        {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM bookings WHERE booking_id = @bookingId;";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@bookingId", bookingId);

            try
            {
                Connection.Open();
                SqlDataReader sqlDataReader = Command.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    isFound = true;
                    bookingPrice = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("booking_price"));
                    touristId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("tourist_id"));
                    tripId = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("trip_id"));
                    billTypeId = sqlDataReader.GetInt16(sqlDataReader.GetOrdinal("bill_type_id"));
                    bookingStatusId = sqlDataReader.GetInt16(sqlDataReader.GetOrdinal("booking_status_id"));
                    createdAt = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("created_at"));
                }
                else
                {
                    isFound = false;
                }
                sqlDataReader.Close();
            }
            catch (Exception ex) { }
            finally
            {
                Connection.Close();


            }
            return isFound;
        }

        public static DataTable ListAllBookings()
        {
            DataTable bookingTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM bookings;";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                bookingTable.Load(reader);
                reader.Close();

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return bookingTable;
        }

        public static int AddNewBooking(decimal bookingPrice, int touristId, int tripId, short billTypeId, short bookingStatusId, DateTime createdAt)
        {

            int newBookingId = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO bookings (booking_price, tourist_id, trip_id, bill_type_id, booking_status_id, created_at) 
                             VALUES (@bookingPrice, @touristId, @tripId, @billTypeId, @bookingStatusId, @createdAt);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookingPrice", bookingPrice);
            command.Parameters.AddWithValue("@touristId", touristId);
            command.Parameters.AddWithValue("@tripId", tripId);
            command.Parameters.AddWithValue("@billTypeId", billTypeId);
            command.Parameters.AddWithValue("@bookingStatusId", bookingStatusId);
            command.Parameters.AddWithValue("@createdAt", createdAt);
            try
            {
                connection.Open();
                newBookingId = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return newBookingId;

        }

        public static bool UpdateBooking(int bookingId, decimal bookingPrice, int touristId, int tripId, short billTypeId, short bookingStatusId, DateTime createdAt)
        {
            
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"UPDATE bookings 
                             SET booking_price = @bookingPrice, tourist_id = @touristId, trip_id = @tripId, 
                                 bill_type_id = @billTypeId, booking_status_id = @bookingStatusId, created_at = @createdAt 
                             WHERE booking_id = @bookingId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookingId", bookingId);
            command.Parameters.AddWithValue("@bookingPrice", bookingPrice);
            command.Parameters.AddWithValue("@touristId", touristId);
            command.Parameters.AddWithValue("@tripId", tripId);
            command.Parameters.AddWithValue("@billTypeId", billTypeId);
            command.Parameters.AddWithValue("@bookingStatusId", bookingStatusId);
            command.Parameters.AddWithValue("@createdAt", createdAt);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);

        }

        public static bool DeleteBooking(int bookingId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"DELETE FROM bookings WHERE booking_id = @bookingId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookingId", bookingId);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool IsBookingExists(int bookingId)
        {
            bool isExists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT COUNT(*) FROM bookings WHERE booking_id = @bookingId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookingId", bookingId);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isExists = (count > 0);
            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }
            return isExists;
        }















        }
}
