using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.TripsData;

namespace nawras_business_layer.Trips
{

    public class clsTrips
    {

        private enum enMode { enAddTrip = 1, enUpdateTrip = 2 };
        private enMode _Mode = enMode.enAddTrip;



        public int TripId { get; set; }
        public string TripName { get; set; }
        public string TripStartDestination { get; set; }
        public string TripEndDestination { get; set; }
        public string TripReturnDestination { get; set; }
        public int VesselId { get; set; }
        public DateTime TripStartDate { get; set; }
        public DateTime TripEndDate { get; set; }
        public short AverageRating { get; set; }
        public decimal TripPrice { get; set; }
        public int TripOwnerId { get; set; }
        public short TripStatusId { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsTrips()
        {
            TripId = -1;
            TripName = string.Empty;
            TripStartDestination = string.Empty;
            TripEndDestination = string.Empty;
            TripReturnDestination = string.Empty;
            VesselId = -1;
            TripStartDate = DateTime.MinValue;
            TripEndDate = DateTime.MinValue;
            AverageRating = 0;
            TripPrice = 0.0m;
            TripOwnerId = -1;
            TripStatusId = -1;
            CreatedAt = DateTime.MinValue;

            _Mode = enMode.enAddTrip;
        }

        public clsTrips(int tripId, string tripName, string tripStartDestination, string tripEndDestination, string tripReturnDestination, int vesselId, DateTime tripStartDate, DateTime tripEndDate,
            short averageRating, decimal tripPrice, int tripOwnerId, short tripStatusId, DateTime createdAt)
        {
            TripId = tripId;
            TripName = tripName;
            TripStartDestination = tripStartDestination;
            TripEndDestination = tripEndDestination;
            TripReturnDestination = tripReturnDestination;
            VesselId = vesselId;
            TripStartDate = tripStartDate;
            TripEndDate = tripEndDate;
            AverageRating = averageRating;
            TripPrice = tripPrice;
            TripOwnerId = tripOwnerId;
            TripStatusId = tripStatusId;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdateTrip;
        }

        public static clsTrips FindTripById(int tripId)
        {

            string tripName = string.Empty;
            string tripStartDestination = string.Empty;
            string tripEndDestination = string.Empty;
            string tripReturnDestination = string.Empty;
            int vesselId = -1;
            DateTime tripStartDate = DateTime.MinValue;
            DateTime tripEndDate = DateTime.MinValue;
            short averageRating = 0;
            decimal tripPrice = 0.0m;
            int tripOwnerId = -1;
            short tripStatusId = -1;
            DateTime createdAt = DateTime.MinValue;

            if (clsTripsDataAccess.GetTripInfoById(tripId, ref tripName, ref tripStartDestination, ref tripEndDestination, ref tripReturnDestination, ref vesselId,
                ref tripStartDate, ref tripEndDate, ref averageRating, ref tripPrice, ref tripOwnerId, ref tripStatusId, ref createdAt))
            {
                return new clsTrips(tripId, tripName, tripStartDestination, tripEndDestination, tripReturnDestination, vesselId,
                    tripStartDate, tripEndDate, averageRating, tripPrice, tripOwnerId, tripStatusId, createdAt);
            }
            else
            {
                return null;
            }


        }

        public static DataTable ListAllTrips()
        {
            return clsTripsDataAccess.ListAllTrips();
        }

        private bool _AddNewTrip()
        {
            this.TripId = clsTripsDataAccess.AddNewTrip(this.TripName, this.TripStartDestination, this.TripEndDestination, this.TripReturnDestination,
                this.VesselId, this.TripStartDate, this.TripEndDate, this.AverageRating, this.TripPrice, this.TripOwnerId, this.TripStatusId, this.CreatedAt);
            return (this.TripId != 0);
        }

        private bool _UpdateTrip()
        {
            return clsTripsDataAccess.UpdateTrip(this.TripId, this.TripName, this.TripStartDestination, this.TripEndDestination, this.TripReturnDestination,
                this.VesselId, this.TripStartDate, this.TripEndDate, this.AverageRating, this.TripPrice, this.TripOwnerId, this.TripStatusId, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddTrip:
                    if (_AddNewTrip())
                    {
                        _Mode = enMode.enUpdateTrip; // Change mode to update after adding
                        return true; // Indicate that the trip was added successfully

                    }
                    else
                    {
                        return false; // Indicate that the trip was not added successfully
                    }

                case enMode.enUpdateTrip:
                    return _UpdateTrip();
            }

            return true;

        }

        public static bool DeleteTrip(int tripId)
        {
            return clsTripsDataAccess.DeleteTrip(tripId);
        }

        public static bool IsTripExists(int tripId)
        {
            return clsTripsDataAccess.IsTripExist(tripId);
        }









        }
}
