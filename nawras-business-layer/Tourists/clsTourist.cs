using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.TouristsData;

namespace nawras_business_layer
{
    public class clsTourist
    {



        private enum enMode { enAddTourist = 1, enUpdateTourist = 2 };
        private enMode _Mode = enMode.enAddTourist;


        public int TouristId { get; set; }
        public int PersonId { get; set; }
        public int StandardUserId { get; set; }
        public string PassportNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsTourist()
        {
            TouristId = -1;
            PersonId = -1;
            StandardUserId = -1;
            PassportNumber = string.Empty;
            CreatedAt = DateTime.MinValue;

            _Mode = enMode.enAddTourist;
        }

        public clsTourist(int touristId, int personId, int standardUserId, string passportNumber, DateTime createdAt)
        {
            TouristId = touristId;
            PersonId = personId;
            StandardUserId = standardUserId;
            PassportNumber = passportNumber;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdateTourist;
        }

        public static clsTourist FindTouristById(int touristId)
        {
            int personId = -1;
            int standardUserId = -1;
            string passportNumber = string.Empty;
            DateTime createdAt = DateTime.MinValue;

            if (clsTouristDataAccess.GetTouristInfoById(touristId, ref personId, ref standardUserId, ref passportNumber, ref createdAt))
            {
                return new clsTourist(touristId, personId, standardUserId, passportNumber, createdAt);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListAllTourists()
        {
            return clsTouristDataAccess.ListAllTourists();
        }

        private bool _AddNewTourst()
        {
            this.TouristId = clsTouristDataAccess.AddTourist(this.PersonId, this.StandardUserId, this.PassportNumber, this.CreatedAt);

            return (this.TouristId != -1);
        }

        private bool _UpdateTourist()
        {
            return clsTouristDataAccess.UpdateTourist(this.TouristId, this.PersonId, this.StandardUserId, this.PassportNumber, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddTourist:
                    if (_AddNewTourst())
                    {
                        _Mode = enMode.enUpdateTourist; // Change mode to update after adding
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateTourist:
                    return _UpdateTourist();

            }

            return false;
        }

        public static bool Delete(int touristId)
        {
            return clsTouristDataAccess.DeleteTourist(touristId);
        }

        public static bool IsTouristExists(int touristId)
        {
            return clsTouristDataAccess.IsTouristExist(touristId);
        }













        }
}
