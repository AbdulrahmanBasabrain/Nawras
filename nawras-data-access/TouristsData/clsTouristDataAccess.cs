using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.TouristsData
{
    public static class clsTouristDataAccess
    {




        public static bool GetTouristInfoById(int touristId, ref int personId, ref int standardUserId, ref string passportNumber, ref DateTime createdAt)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " select * from tourists where tourist_id = @touristId;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@touristId", touristId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    personId = reader.GetInt32(reader.GetOrdinal(@"personId"));
                    standardUserId = reader.GetInt32(reader.GetOrdinal(@"standardUserId"));
                    passportNumber = reader.GetString(reader.GetOrdinal(@"passportNumber"));
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

        public static DataTable ListAllTourists()
        {
            DataTable touristsDataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from tourists;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                touristsDataTable.Load(reader);
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
            return touristsDataTable;

        }

        public static int AddTourist(int personId, int standardUserId, string passportNumber, DateTime createdAt)
        {
            int touristId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
GO

INSERT INTO [dbo].[tourists]
           ([person_id]
           ,[standard_user_id]
           ,[passport_number]
           ,[created_at])
     VALUES
           (@personId
           ,@standardUserId
           ,@passportNumber
           ,@createdAt)
GO

";



            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@standardUserId", standardUserId);
            command.Parameters.AddWithValue("@passportNumber", passportNumber);
            command.Parameters.AddWithValue("@createdAt", createdAt);

            try
            {
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    touristId = insertedId;
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
            return touristId;

        }

        public static bool UpdateTourist(int touristId, int personId, int standardUserId, string passportNumber, DateTime createdAt)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"

GO

UPDATE [dbo].[tourists]
   SET [person_id] = @personId
      ,[standard_user_id] = @standardUserId
      ,[passport_number] = @passportNumber
      ,[created_at] = @createdAt
 WHERE tourist_id = @touristId
GO

";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@touristId", touristId);
            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@standardUserId", standardUserId);
            command.Parameters.AddWithValue("@passportNumber", passportNumber);
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

        public static bool DeleteTourist(int touristId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
DELETE FROM [dbo].[captains]
      WHERE tourist_id = @touristId

";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@touristId", touristId);

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

        public static bool IsTouristExist(int touristId)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
select * FROM [dbo].[captains]
      WHERE tourist_id = @touristId

";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@touristId", touristId);

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
