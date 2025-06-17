using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.StandardUsersData
{
    public static class clsStandardUserDataAccess
    {




        public static bool GetStandardUserInfoById(int standardUserId, ref int employeeId, ref string userName, ref string password, ref bool isActive, ref int permession, ref DateTime createdAt)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from standard_users where standard_user_id = @standardUserId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"standardUserId", standardUserId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    employeeId = (int)reader["employeeId"];
                    userName = (string)reader["user_name"];
                    password = (string)reader["password"];
                    isActive = (bool)reader["is_active"];
                    permession = (int)reader["permession"];
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

        public static DataTable ListAllStandardUseres()
        {
            DataTable systemUsersTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Select * from standard_users";

            SqlCommand command = new SqlCommand(@query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    systemUsersTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return systemUsersTable;
        }

        public static int AddNewStandardUser(int employeeId, string userName, string password, int permession, bool isActive, DateTime createdAt)
        {

            int systemUserId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"insert 
                                INSERT INTO [dbo].[standard_users]
           ([employee_id]
           ,[username]
           ,[password_hash]
           ,[is_active]
           ,[persmession]
           ,[created_at])
     VALUES

(@employeeId, @userName, @password, @permession, @isActive, @createdAt)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);
            command.Parameters.AddWithValue(@"userName", userName);
            command.Parameters.AddWithValue(@"password", password);
            command.Parameters.AddWithValue(@"permession", permession);
            command.Parameters.AddWithValue(@"isActive", isActive);
            command.Parameters.AddWithValue(@"createdAt", createdAt);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    systemUserId = insertedId;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return systemUserId;

        }

        public static bool UpdateStandardUser(int standardUserId, int employeeId, string userName, string password, int permession, bool isActive, DateTime createdAt)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"GO

UPDATE [dbo].[standard_users]
   SET [employee_id] = @employeeId
      ,[username] = @userName
      ,[password_hash] = @password
      ,[is_active] = @isActive
      ,[persmession] = @permession
      ,[created_at] = @createdAt
 WHERE standard_user_id = @systemUserId
GO";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"standardUserId", standardUserId);
            command.Parameters.AddWithValue(@"employeeId", employeeId);
            command.Parameters.AddWithValue(@"userName", userName);
            command.Parameters.AddWithValue(@"password", password);
            command.Parameters.AddWithValue(@"permession", permession);
            command.Parameters.AddWithValue(@"isActive", isActive);
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

        public static bool DeleteStandardUser(int standardUserId)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"GO

DELETE FROM [dbo].[standard_users]
      where standard_user_id = @systemUserId
GO";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"systemUserId", standardUserId);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return (rowsAffected > 0);
        }

        public static bool isExist(int standardUserId)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from standard_users where standard_user_id = @standardUserId";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"standardUserId", standardUserId);

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

        public static bool IsActive(int standardUserId)
        {
            bool isActive = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select active = 1 from standard_users where standard_user_id = @systemUserId and is_active = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"standardUserId", standardUserId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                isActive = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isActive;



        }




























    }
}
