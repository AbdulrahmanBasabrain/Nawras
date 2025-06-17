using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.vesselsData
{
    public static class clsVesselTypeDataAccess
    {

        public static bool GetVesselTypeInfoById(int vesselTypeId, ref int vesselMaxCapacity, ref string vesselTypeName, ref string vesselTypeDescription, ref bool hasCabins)
        {
            bool isFound = false;


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from vessel_types where vessel_type_id = @vesselTypeId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselTypeId", vesselTypeId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    isFound = true;
                    while (reader.Read())
                    {
                        vesselMaxCapacity = reader.GetInt32(1);
                        vesselTypeName = reader.GetString(2);
                        vesselTypeDescription = reader.GetString(3);
                        hasCabins = reader.GetBoolean(4);
                    }
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in GetVesselTypeInfoById: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable ListAllVesselTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from vessel_types";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { throw new Exception("Error in ListAllVesselTypes: " + ex.Message); }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static int AddNewVesselType(int vesselMaxCapacity, string vesselTypeName, string vesselTypeDescription, bool hasCabins)
        {
            int vesselTypeId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into vessel_types (vessel_max_capacity, vessel_type_name, vessel_type_description, has_cabins) 
                             values (@vesselMaxCapacity, @vesselTypeName, @vesselTypeDescription, @hasCabins)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselMaxCapacity", vesselMaxCapacity);
            command.Parameters.AddWithValue("@vesselTypeName", vesselTypeName);
            command.Parameters.AddWithValue("@vesselTypeDescription", vesselTypeDescription);
            command.Parameters.AddWithValue("@hasCabins", hasCabins);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedId))
                {
                    vesselTypeId = insertedId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddNewVesselType: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return vesselTypeId;
        }

        public static bool UpdateVesselType(int vesselTypeId, int vesselMaxCapacity, string vesselTypeName, string vesselTypeDescription, bool hasCabins)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update vessel_types set vessel_max_capacity = @vesselMaxCapacity, 
                             vessel_type_name = @vesselTypeName, 
                             vessel_type_description = @vesselTypeDescription, 
                             has_cabins = @hasCabins 
                             where vessel_type_id = @vesselTypeId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselTypeId", vesselTypeId);
            command.Parameters.AddWithValue("@vesselMaxCapacity", vesselMaxCapacity);
            command.Parameters.AddWithValue("@vesselTypeName", vesselTypeName);
            command.Parameters.AddWithValue("@vesselTypeDescription", vesselTypeDescription);
            command.Parameters.AddWithValue("@hasCabins", hasCabins);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateVesselType: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);

        }

        public static bool DeleteVesselType(int vesselTypeId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from vessel_types where vessel_type_id = @vesselTypeId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselTypeId", vesselTypeId);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteVesselType: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool IsVesselTypeExists(int vesselTypeId)
        {
            bool exists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found = 1 from vessel_types where vessel_type_id = @vesselTypeId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselTypeId", vesselTypeId);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                exists = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Error in IsVesselTypeExists: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return exists;

        }
        


    }
}
