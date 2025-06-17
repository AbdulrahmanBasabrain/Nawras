using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.CaptinsData;

namespace nawras_business_layer.Captins
{
    public class clsCaptain
    {

        private enum enMode { enAddCaptain = 1, enUpdateCaptain = 2 };
        private enMode _mode = enMode.enAddCaptain;

        public int CaptainId { get; set; }
        public int PersonId { get; set; }
        public int StandardUserId { get; set; }
        public DateTime StartedSailing { get; set; }
        public string LicenseNumber { get; set; }
        public short Rating { get; set; }
        public int TripsCompleted { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsCaptain()
        {
            CaptainId = -1;
            PersonId = -1;
            StandardUserId = -1;
            StartedSailing = DateTime.MinValue;
            LicenseNumber = string.Empty;
            Rating = -1;
            TripsCompleted = -1;
            IsAvailable = false;
            CreatedAt = DateTime.MinValue;

            _mode = enMode.enAddCaptain;
        }

        public clsCaptain(int captainId, int personId, int standardUserId, DateTime startedSailing, string licenseNumber, short rating, int tripsCompleted, bool isAvailable, DateTime createdAt)
        {
            CaptainId = captainId;
            PersonId = personId;
            StandardUserId = standardUserId;
            StartedSailing = startedSailing;
            LicenseNumber = licenseNumber;
            Rating = rating;
            TripsCompleted = tripsCompleted;
            IsAvailable = isAvailable;
            CreatedAt = createdAt;


            _mode = enMode.enUpdateCaptain;
        }

        public static clsCaptain FindCaptainById(int captainId)
        {
            int personId = -1;
            int standardUserId = -1;
            DateTime startedSailing = DateTime.MinValue;
            string licenseNumber = string.Empty;
            short rating = -1;
            int tripsCompleted = -1;
            bool isAvailable = false;
            DateTime createdAt = DateTime.MinValue;

            if (clsCaptainDataAccess.GetCaptainInfoById(captainId, ref personId, ref standardUserId, ref startedSailing, ref licenseNumber,
                ref rating, ref tripsCompleted, ref isAvailable, ref createdAt))
            {
                return new clsCaptain(captainId, personId, standardUserId, startedSailing, licenseNumber, rating, tripsCompleted, isAvailable, createdAt);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListAllCaptains()
        {
            return clsCaptainDataAccess.ListAllCaptains();
        }

        private bool _AddNewCaptain()
        {
            this.CaptainId = clsCaptainDataAccess.AddCaptain(this.PersonId, this.StandardUserId, this.StartedSailing, this.LicenseNumber, this.Rating, this.TripsCompleted, this.IsAvailable, this.CreatedAt);

            return (this.CaptainId != -1);
        }

        private bool _UpdateCaptain()
        {
            return clsCaptainDataAccess.UpdateCaptain(this.CaptainId, this.PersonId, this.StandardUserId, this.StartedSailing, this.LicenseNumber, this.Rating, this.TripsCompleted, this.IsAvailable, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.enAddCaptain:
                    if (_AddNewCaptain())
                    {
                        _mode = enMode.enUpdateCaptain; // Switch to update mode after adding
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateCaptain:
                    return _UpdateCaptain();
            }
            return false;
        }

        public static bool DeleteCaptain(int captainId)
        {
            return clsCaptainDataAccess.DeleteCaptain(captainId);
        }

        public static bool IsCaptainAvailable(int captainId)
        {
            return clsCaptainDataAccess.IsCaptainAvailable(captainId);
        }

        public static bool IsCaptainExists(int captainId)
        {
            return clsCaptainDataAccess.IsCaptainExist(captainId);
        }





    }
}
