using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.TripsData
{
    public static class clsTripsDataAccess
    {


        public static bool GetTripInfoById(int tripId, ref string tripName, ref string tripStartDestination, ref string tripEndDestination, ref string tripReturnDestination,
            ref int vesselId, ref DateTime tripStartDate, ref DateTime tripEndDate, ref short averageRating, ref decimal tripPrice,
            ref int tripOwnerId, ref short tripStatusId, ref DateTime createdAt)
        {


            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " select * from trips where trip_id = @tripId;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@tripId", tripId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    tripName = reader.GetString(reader.GetOrdinal(@"tripName"));
                    tripStartDestination = reader.GetString(reader.GetOrdinal(@"tripStartDestination"));
                    tripEndDestination = reader.GetString(reader.GetOrdinal(@"tripEndDestination"));
                    tripReturnDestination = reader.GetString(reader.GetOrdinal(@"tripReturnDestination"));
                    vesselId = reader.GetInt32(reader.GetOrdinal(@"vesselId"));
                    tripStartDate = reader.GetDateTime(reader.GetOrdinal(@"tripStartDate"));
                    tripEndDate = reader.GetDateTime(reader.GetOrdinal(@"tripEndDate"));
                    averageRating = reader.GetInt16(reader.GetOrdinal(@"averageRating"));
                    tripPrice = reader.GetDecimal(reader.GetOrdinal(@"tripPrice"));
                    tripOwnerId = reader.GetInt32(reader.GetOrdinal(@"tripOwnerId"));
                    tripStatusId = reader.GetInt16(reader.GetOrdinal(@"tripStatusId"));
                    createdAt = reader.GetDateTime(reader.GetOrdinal(@"createdAt"));
                }
                else
                {
                    isFound = false;
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
            return isFound;



        }

        public static DataTable ListAllTrips()
        {
            DataTable tripsDataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from trips;";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                tripsDataTable.Load(reader);
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
            return tripsDataTable;
        }


        public static int AddNewTrip(string tripName, string tripStartDestination, string tripEndDestination, string tripReturnDestination,
            int vesselId, DateTime tripStartDate, DateTime tripEndDate, short averageRating, decimal tripPrice,
            int tripOwnerId, short tripStatusId, DateTime createdAt)
        {
            int tripId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "insert into trips (tripName, tripStartDestination, tripEndDestination, tripReturnDestination, vesselId, " +
                "tripStartDate, tripEndDate, averageRating, tripPrice, tripOwnerId, tripStatusId, createdAt) " +
                "output inserted.trip_id values (@tripName, @tripStartDestination, @tripEndDestination, @tripReturnDestination, @vesselId," +
                "@tripStartDate, @tripEndDate, @averageRating, @tripPrice, @tripOwnerId, @tripStatusId, @createdAt);";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@tripName", tripName);
            command.Parameters.AddWithValue("@tripStartDestination", tripStartDestination);
            command.Parameters.AddWithValue("@tripEndDestination", tripEndDestination);
            command.Parameters.AddWithValue("@tripReturnDestination", tripReturnDestination);
            command.Parameters.AddWithValue("@vesselId", vesselId);
            command.Parameters.AddWithValue("@tripStartDate", tripStartDate);
            command.Parameters.AddWithValue("@tripEndDate", tripEndDate);
            command.Parameters.AddWithValue("@averageRating", averageRating);
            command.Parameters.AddWithValue("@tripPrice", tripPrice);
            command.Parameters.AddWithValue("@tripOwnerId", tripOwnerId);
            command.Parameters.AddWithValue("@tripStatusId", tripStatusId);
            command.Parameters.AddWithValue("@createdAt", createdAt);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    tripId = Convert.ToInt32(result);
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
            return tripId;
        }

        public static bool UpdateTrip(int tripId, string tripName, string tripStartDestination, string tripEndDestination, string tripReturnDestination,
            int vesselId, DateTime tripStartDate, DateTime tripEndDate, short averageRating, decimal tripPrice,
            int tripOwnerId, short tripStatusId, DateTime createdAt)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"

               GO

UPDATE [dbo].[trips]
   SET [trip_name] = <trip_name, nvarchar(500),>
      ,[trip_start_destination] = <trip_start_destination, nvarchar(500),>
      ,[trip_end_destination] = <trip_end_destination, nvarchar(500),>
      ,[trip_return_destination] = <trip_return_destination, nvarchar(500),>
      ,[vessel_id] = <vessel_id, int,>
      ,[trip_start_date] = <trip_start_date, datetime2(7),>
      ,[trip_end_date] = <trip_end_date, datetime2(7),>
      ,[average_rating] = <average_rating, smallint,>
      ,[trip_price] = <trip_price, smallmoney,>
      ,[trip_representative_owner_id] = <trip_representative_owner_id, int,>
      ,[trip_status_id] = <trip_status_id, smallint,>
      ,[created_at] = <created_at, datetime2(7),>
 WHERE trip_id = @tripId 
GO 

";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@tripId", tripId);
            command.Parameters.AddWithValue("@tripName", tripName);
            command.Parameters.AddWithValue("@tripStartDestination", tripStartDestination);
            command.Parameters.AddWithValue("@tripEndDestination", tripEndDestination);
            command.Parameters.AddWithValue("@tripReturnDestination", tripReturnDestination);
            command.Parameters.AddWithValue("@vesselId", vesselId);
            command.Parameters.AddWithValue("@tripStartDate", tripStartDate);
            command.Parameters.AddWithValue("@tripEndDate", tripEndDate);
            command.Parameters.AddWithValue("@averageRating", averageRating);
            command.Parameters.AddWithValue("@tripPrice", tripPrice);
            command.Parameters.AddWithValue("@tripOwnerId", tripOwnerId);
            command.Parameters.AddWithValue("@tripStatusId", tripStatusId);
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

        public static bool DeleteTrip(int tripId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "delete from trips where trip_id = @tripId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@tripId", tripId);
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

        public static bool IsTripExist(int tripId)
        {


            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select count(*) from trips where trip_id = @tripId;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@tripId", tripId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isExist = reader.HasRows;
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }
            return isExist;

        }

    }
}
