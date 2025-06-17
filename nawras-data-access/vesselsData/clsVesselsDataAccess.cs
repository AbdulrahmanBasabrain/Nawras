using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nawras_data_access.vesselsData
{
    public static class clsVesselsDataAccess
    {


        public static bool GetVesselInfoById(int vesselId, ref int vesselTypeId, ref short vesselRating, ref DateTime listingDate, ref int vesselOwnerId, ref DateTime createdAt)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from vessels where vessel_id = @vesselId";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@vesselId", vesselId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    isFound = true;
                    while (reader.Read())
                    {
                        vesselTypeId = reader.GetInt32(1);
                        vesselRating = reader.GetInt16(2);
                        listingDate = reader.GetDateTime(3);
                        vesselOwnerId = reader.GetInt32(4);
                        createdAt = reader.GetDateTime(5);
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
                throw new Exception("Error in GetVesselInfoById: " + ex.Message);
            }
            finally
            {
                connection.Close();

            }
            return isFound;

        }

        public static DataTable ListAllVessels()
        {
            DataTable vesselDataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from vessels";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                vesselDataTable.Load(reader);
                reader.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Error in ListAllVessels: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return vesselDataTable;
        }


        public static int AddNewVessel(int vesselTypeId, short vesselRating, DateTime listingDate, int vesselOwnerId, DateTime createdAt)
        {
            int newVesselId = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into vessels (vessel_type_id, vessel_rating, listing_date, vessel_owner_id, created_at) 
                             output inserted.vessel_id 
                             values (@vesselTypeId, @vesselRating, @listingDate, @vesselOwnerId, @createdAt)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@vesselTypeId", vesselTypeId);
            command.Parameters.AddWithValue("@vesselRating", vesselRating);
            command.Parameters.AddWithValue("@listingDate", listingDate);
            command.Parameters.AddWithValue("@vesselOwnerId", vesselOwnerId);
            command.Parameters.AddWithValue("@createdAt", createdAt);
            try
            {
                connection.Open();
                newVesselId = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddNewVessel: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return newVesselId;
        }


        public static bool UpdateVessel(int vesselId, int vesselTypeId, short vesselRating, DateTime listingDate, int vesselOwnerId, DateTime createdAt)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"update vessels 
                             set vessel_type_id = @vesselTypeId, 
                                 vessel_rating = @vesselRating, 
                                 listing_date = @listingDate, 
                                 vessel_owner_id = @vesselOwnerId, 
                                 created_at = @createdAt 
                             where vessel_id = @vesselId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselId", vesselId);
            command.Parameters.AddWithValue("@vesselTypeId", vesselTypeId);
            command.Parameters.AddWithValue("@vesselRating", vesselRating);
            command.Parameters.AddWithValue("@listingDate", listingDate);
            command.Parameters.AddWithValue("@vesselOwnerId", vesselOwnerId);
            command.Parameters.AddWithValue("@createdAt", createdAt);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in UpdateVessel: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool DeleteVessel(int vesselId)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete from vessels where vessel_id = @vesselId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselId", vesselId);
            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in DeleteVessel: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowsAffected > 0);
        }

        public static bool IsVesselExists(int vesselId)
        {
            bool exists = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select count(*) from vessels where vessel_id = @vesselId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@vesselId", vesselId);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                exists = (count > 0);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in IsVesselExists: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return exists;
        }


    }
}
