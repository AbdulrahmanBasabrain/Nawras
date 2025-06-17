using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using nawras_data_access.vesselsData;

namespace nawras_business_layer.Vessels
{
    public class clsVessels
    {

        private enum enMode { enAddNewVessel = 1, enUpdateVessel = 2 }
        private enMode _mode;

        public int VesselId { get; set; }
        public int VesselTypeId { get; set; }
        public short VesselRating { get; set; }
        public DateTime ListingDate { get; set; }
        public int VesselOwnerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsVessels()
        {
            VesselId = 0;
            VesselTypeId = 0;
            VesselRating = 0;
            ListingDate = SqlDateTime.MinValue.Value;
            VesselOwnerId = 0;
            CreatedAt = SqlDateTime.MinValue.Value;

            _mode = enMode.enAddNewVessel;
        }

        public clsVessels(int vesselId, int vesselTypeId, short vesselRating, DateTime listingDate, int vesselOwnerId, DateTime createdAt)
        {
            VesselId = vesselId;
            VesselTypeId = vesselTypeId;
            VesselRating = vesselRating;
            ListingDate = listingDate;
            VesselOwnerId = vesselOwnerId;
            CreatedAt = createdAt;

            _mode = enMode.enUpdateVessel;
        }

        public static clsVessels FindVesselById(int vesselId)
        {
            int vesselTypeId = 0;
            short vesselRating = 0;
            DateTime listingDate = SqlDateTime.MinValue.Value;
            int vesselOwnerId = 0;
            DateTime createdAt = SqlDateTime.MinValue.Value;

            if (clsVesselsDataAccess.GetVesselInfoById(vesselId, ref vesselTypeId, ref vesselRating, ref listingDate, ref vesselOwnerId, ref createdAt))
            {
                return new clsVessels(vesselId, vesselTypeId, vesselRating, listingDate, vesselOwnerId, createdAt);
            }
            else
            {
                return null; // or throw an exception if preferred
            }
        }

        public static DataTable ListAllVessels()
        {
            return clsVesselsDataAccess.ListAllVessels();
        }

        private bool AddNewVessel()
        {

            this.VesselId = clsVesselsDataAccess.AddNewVessel(this.VesselTypeId, this.VesselRating, this.ListingDate, this.VesselOwnerId, this.CreatedAt);

            return (this.VesselId != -1);
        }

        private bool UpdateVessel()
        {
            return clsVesselsDataAccess.UpdateVessel(this.VesselId, this.VesselTypeId, this.VesselRating, this.ListingDate, this.VesselOwnerId, this.CreatedAt);
        }

        public bool Save()
        {

            switch (_mode)
            {
                case enMode.enAddNewVessel:
                    if (this.AddNewVessel())
                    {
                        _mode = enMode.enUpdateVessel;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateVessel:
                    return this.UpdateVessel();

            }

            return false; // Should not reach here, but just in case    

        }

        public static bool DeleteVessel(int vesselId)
        {
            return clsVesselsDataAccess.DeleteVessel(vesselId);
        }

        public static bool IsVesselIdExists(int vesselId)
        {
            return clsVesselsDataAccess.IsVesselIdExists(vesselId);
        }


    }
}
