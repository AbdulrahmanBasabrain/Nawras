using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.CaptinsData
{
    public static class clsCaptainDataAccess
    {




        public static bool GetCaptainInfoById(int captainId, ref int personId, ref int standardUserId, ref DateTime startedSailing, ref string licenseNumber,
            ref short rating, ref int tripsCompleted, ref bool isAvailable, ref DateTime createdAt)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " select * from Captains where CaptainId = @captainId;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@captainId", captainId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    personId = reader.GetInt32(reader.GetOrdinal(@"personId"));
                    standardUserId = reader.GetInt32(reader.GetOrdinal(@"standardUserId"));
                    startedSailing = reader.GetDateTime(reader.GetOrdinal(@"startedSailing"));
                    licenseNumber = reader.GetString(reader.GetOrdinal(@"LicenseNumber"));
                    rating = reader.GetInt16(reader.GetOrdinal(@"rating"));
                    tripsCompleted = reader.GetInt32(reader.GetOrdinal(@"TripsCompleted"));
                    isAvailable = reader.GetBoolean(reader.GetOrdinal(@"isAvailable"));
                    createdAt = reader.GetDateTime(reader.GetOrdinal(@"createdAt"));
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;

        }

        public static DataTable ListAllCaptains()
        {
            DataTable captainsDataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from captains;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                captainsDataTable.Load(reader);
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
            return captainsDataTable;

        }

        public static int AddCaptain(int personId, int standardUserId, DateTime startedSailing, string licenseNumber, short rating, int tripsCompleted, bool isAvailable, DateTime createdAt)
        {
            int captainId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
GO

INSERT INTO [dbo].[captains]
           ([person_id]
           ,[standard_user_id]
           ,[started_sailing]
           ,[license_number]
           ,[rating]
           ,[trips_completed]
           ,[is_available]
           ,[created_at])
     VALUES
           (@personId
           ,@standardUserId
           ,@startedSailing
           ,@licenseNumber
           ,@rating
           ,@tripsCompleted
           ,@isAvailable
           ,@createdAt)
GO

";



            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@standardUserId", standardUserId);
            command.Parameters.AddWithValue("@startedSailing", startedSailing);
            command.Parameters.AddWithValue("@licenseNumber", licenseNumber);
            command.Parameters.AddWithValue("@rating", rating);
            command.Parameters.AddWithValue("@tripsCompleted", tripsCompleted);
            command.Parameters.AddWithValue("@isAvailable", isAvailable);
            command.Parameters.AddWithValue("@createdAt", createdAt);

            try
            {
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    captainId = insertedId;
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
            return captainId;

        }

        public static bool UpdateCaptain(int captainId, int personId, int standardUserId, DateTime startedSailing, string licenseNumber, short rating, int tripsCompleted, bool isAvailable, DateTime createdAt)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"

GO

UPDATE [dbo].[captains]
   SET [person_id] = @personId
      ,[standard_user_id] = @standardUserId
      ,[started_sailing] = @startedSailing
      ,[license_number] = @licenseNumber
      ,[rating] = @rating
      ,[trips_completed] = @tripsCompleted
      ,[is_available] = @isAvailable
      ,[created_at] = @createdAt
 WHERE captain_id = @captainId
GO

";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@captainId", captainId);
            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@standardUserId", standardUserId);
            command.Parameters.AddWithValue("@startedSailing", startedSailing);
            command.Parameters.AddWithValue("@licenseNumber", licenseNumber);
            command.Parameters.AddWithValue("@rating", rating);
            command.Parameters.AddWithValue("@tripsCompleted", tripsCompleted);
            command.Parameters.AddWithValue("@isAvailable", isAvailable);
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

        public static bool DeleteCaptain(int captainId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
DELETE FROM [dbo].[captains]
      WHERE captain_id = @captainId

";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@captainId", captainId);

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

        public static bool IsCaptainExist(int captainId)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
select * FROM [dbo].[captains]
      WHERE captain_id = @captainId

";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@captainId", captainId);

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

        public static bool IsCaptainAvailable(int captainId)
        {

            bool isAvailable = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
select * FROM [dbo].[captains]
      WHERE captain_id = @captainId and is_available = 1

";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@captainId", captainId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isAvailable = reader.HasRows;
            }
            catch (Exception ex)
            {
                // Handle exception (log it, rethrow it, etc.)
            }
            finally
            {
                connection.Close();
            }
            return isAvailable;
        }

















    }
}
