using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.EmployeesData
{
    public static class clsEmployeeDataAccess
    {




        public static bool GetEmployeeInfoById(int employeeId, ref int personId, ref decimal salary, ref string job, ref DateTime createdAt)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from Employees employee_id = @employeeId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    personId = (int)reader["person_id"];
                    salary = (decimal)reader["salary"];
                    job = (string)reader["job"];
                    createdAt = (DateTime)reader["created_at"];
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

        public static int AddNewEmployee(int personId, decimal salary, string job, DateTime createdAt)
        {
            int employeeId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into employees (person_id, salary, job, created_at) values (@personId, @salary, @job, @createdAt) select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"personId", personId);
            command.Parameters.AddWithValue(@"salary", salary);
            command.Parameters.AddWithValue(@"job", job);
            command.Parameters.AddWithValue(@"createdAt", createdAt);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                connection.Close();

                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    employeeId = insertedId;
                }
            }

            catch (Exception ex) { }
            finally { connection.Close(); }
            return employeeId;
        }

        public static bool UpdateEmployee(int employeeId, int personId, decimal salary, string job, DateTime createdAt)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[employees]
   SET [person_id] = @personId
      ,[salary] = @salary
      ,[job] = @job
     ,[created_at] = @createdAt
 WHERE employee_id = @employeeId";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(@"employeeId", employeeId);
            command.Parameters.AddWithValue(@"personId", personId);
            command.Parameters.AddWithValue(@"salary", salary);
            command.Parameters.AddWithValue(@"job", job);
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

        public static DataTable ListAllEmployees()
        {
            DataTable employeesTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from employees";

            SqlCommand command = new SqlCommand(@query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    employeesTable.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return employeesTable;
        }

        public static bool IsEmployeeExist(int employeeId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select found = 1 from employees where tenant_id = @employeeId";

            bool isFound = false;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue(@"employeeId", employeeId);

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

        public static bool DeleteEmployee(int employeeId)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from employees where employee_id = @employeeId";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue(@"tenantId", employeeId);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return (rowsAffected > 0);

        }


    }


}
