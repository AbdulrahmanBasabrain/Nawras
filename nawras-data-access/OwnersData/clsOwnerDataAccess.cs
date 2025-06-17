using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.OwnersData
{
    public static class clsOwnerDataAccess
    {

        public static bool GetOwnerInfoById(int ownerId, ref int standardUserId, ref int personId, ref bool isBusiness, ref int numberOfAssets, ref DateTime createdAt)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from owners where owner_id = @ownerId";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personId = (int)reader["person_id"];
                    standardUserId = (int)reader["standard_user_id"];
                    isBusiness = (bool)reader["is_business"];
                    numberOfAssets = (int)reader["number_of_assets"];
                    createdAt = (DateTime)reader["created_at"];
                }
                else
                {
                    isFound = false;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;

        }

        public static DataTable ListAllOwners()
        {
            DataTable ownersTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from owners";

            SqlCommand command = new SqlCommand(@query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ownersTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ownersTable;
        }

        public static int AddNewOwner(int personId, int standardUserId, bool isBusiness, int numberOfAssets, DateTime createdAt)
        {
            int ownerId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"GO

INSERT INTO [dbo].[owners]
           ([person_id]
           ,[standard_user_id]
           ,[is_business]
           ,[number_of_assets]
           ,[created_at])
     VALUES
           (@personId
           ,@standardUserId
           ,@isBusiness
           ,@numberOfAssets
           ,@createdAt
GO
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"personId", personId);
            command.Parameters.AddWithValue(@"standardUserId", standardUserId);
            command.Parameters.AddWithValue(@"isBusiness", isBusiness);
            command.Parameters.AddWithValue(@"numberOfAssets", numberOfAssets);
            command.Parameters.AddWithValue(@"createdAt", createdAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    ownerId = insertedId;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ownerId;
        }

        public static bool UpdateOwner(int ownerId, int standardUserId, int personId, bool isBusiness, int numberOfAssets, DateTime createdAt)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"GO

UPDATE [dbo].[owners]
   SET [person_id] = @personId
      ,[standard_user_id] = @standardUserId
      ,[is_business] = @isBusiness
      ,[number_of_assets] = @numberOfAssets
      ,[created_at] = @createdAt
 WHERE owner_id = @ownerId
GO
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);
            command.Parameters.AddWithValue(@"personId", personId);
            command.Parameters.AddWithValue(@"standardUserId", standardUserId);
            command.Parameters.AddWithValue(@"isBusiness", isBusiness);
            command.Parameters.AddWithValue(@"numberOfAssets", numberOfAssets);
            command.Parameters.AddWithValue(@"createdAt", createdAt);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);

        }

        public static bool DeleteOwner(int ownerId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"GO

DELETE FROM [dbo].[owners]
      WHERE owner_id = @ownerId
GO";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);
        }

        public static bool IsOwnerExist(int ownerId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from owners where owner_id = @ownerId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"ownerId", ownerId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;
        }

    }

}
