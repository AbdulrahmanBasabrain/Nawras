using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.BookingsData;

namespace nawras_business_layer.Bookings
{
    public class clsBooking
    {


        private enum enMode { enAddBooking = 1, enUpdateBooking = 2 };
        private enMode _Mode = enMode.enAddBooking;

        public int BookingId { get; set; }
        public decimal BookingPrice { get; set; }
        public int TouristId { get; set; }
        public int TripId { get; set; }
        public short BillTypeId { get; set; }
        public short BookingStatusId { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsBooking()
        {
            BookingId = -1;
            BookingPrice = 0.0m;
            TouristId = -1;
            TripId = -1;
            BillTypeId = -1;
            BookingStatusId = -1;
            CreatedAt = DateTime.MinValue;
            _Mode = enMode.enAddBooking;
        }

        private clsBooking(int bookingId, decimal bookingPrice, int touristId, int tripId, short billTypeId, short bookingStatusId, DateTime createdAt)
        {
            BookingId = bookingId;
            BookingPrice = bookingPrice;
            TouristId = touristId;
            TripId = tripId;
            BillTypeId = billTypeId;
            BookingStatusId = bookingStatusId;
            CreatedAt = createdAt;
            _Mode = enMode.enUpdateBooking;
        }


        public static clsBooking FindBookingById(int bookingId)
        {
            int bookingPrice = -1;
            int touristId = -1;
            int tripId = -1;
            short billTypeId = -1;
            short bookingStatusId = -1;
            DateTime createdAt = DateTime.MinValue;

            if (clsBookingDataAccess.GetBookingInfoById(bookingId, ref bookingPrice, ref touristId, ref tripId, ref billTypeId, ref bookingStatusId, ref createdAt))
            {
                return new clsBooking(bookingId, bookingPrice, touristId, tripId, billTypeId, bookingStatusId, createdAt);
            }
            else
            {
                return null; // Booking not found
            }
        }

        public static DataTable ListAllBookings()
        {
            return clsBookingDataAccess.ListAllBookings();


        }

        private bool _AddNewBooking()
        {
            this.BookingId = clsBookingDataAccess.AddNewBooking(this.BookingPrice, this.TouristId, this.TripId, this.BillTypeId, this.BookingStatusId, this.CreatedAt);

            return (this.BookingId != -1);
        }

        private bool _UpdateBooking()
        {
            return clsBookingDataAccess.UpdateBooking(this.BookingId, this.BookingPrice, this.TouristId, this.TripId, this.BillTypeId, this.BookingStatusId, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddBooking:
                    if (_AddNewBooking())
                    {
                        _Mode = enMode.enUpdateBooking; // Switch to update mode after adding
                        return true;
                    }
                    else
                    {
                        return false; // Indicate that the booking was not added successfully
                    }
                case enMode.enUpdateBooking:
                    return _UpdateBooking();

            }

            return false;
        }

        public static bool Delete(int bookingId)
        {
            return clsBookingDataAccess.DeleteBooking(bookingId);
        }

        public static bool IsBookingExists(int bookingId)
        {
            return clsBookingDataAccess.IsBookingExists(bookingId);
        }


    }
}
