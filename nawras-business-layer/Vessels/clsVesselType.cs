using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.vesselsData;
namespace nawras_business_layer.Vessels
{
    public class clsVesselType
    {

        private enum enMode { enAddNewVesselType = 1, enUpdateVesselType = 2 }
        private enMode _mode;

        public int VesselTypeId { get; set; }
        public int VesselMaxCapacity { get; set; }
        public string VesselTypeName { get; set; }
        public string VesselTypeDescription { get; set; }
        public bool HasCabins { get; set; }

        public clsVesselType()
        {
            VesselTypeId = 0;
            VesselMaxCapacity = 0;
            VesselTypeName = string.Empty;
            VesselTypeDescription = string.Empty;
            HasCabins = false;

            _mode = enMode.enAddNewVesselType;
        }

        public clsVesselType(int vesselTypeId, int vesselMaxCapacity, string vesselTypeName, string vesselTypeDescription, bool hasCabins)
        {
            VesselTypeId = vesselTypeId;
            VesselMaxCapacity = vesselMaxCapacity;
            VesselTypeName = vesselTypeName;
            VesselTypeDescription = vesselTypeDescription;
            HasCabins = hasCabins;

            _mode = enMode.enUpdateVesselType;
        }

        public static clsVesselType FindVesselTypeById(int vesselTypeId)
        {
            int vesselMaxCapacity = 0;
            string vesselTypeName = string.Empty;
            string vesselTypeDescription = string.Empty;
            bool hasCabins = false;
            if (clsVesselTypeDataAccess.GetVesselTypeInfoById(vesselTypeId, ref vesselMaxCapacity, ref vesselTypeName, ref vesselTypeDescription, ref hasCabins))
            {
                return new clsVesselType(vesselTypeId, vesselMaxCapacity, vesselTypeName, vesselTypeDescription, hasCabins);
            }
            else
            {
                return null;
            }
        }


        public static DataTable ListAllVesselTypes()
        {
            return clsVesselTypeDataAccess.ListAllVesselTypes();
        }

        private bool _AddNewVesselType()
        {
            this.VesselTypeId = clsVesselTypeDataAccess.AddNewVesselType(this.VesselMaxCapacity, this.VesselTypeName, this.VesselTypeDescription, this.HasCabins);

            return (VesselTypeId != -1);
        }

        private bool _UpdateVesselType()
        {
            return clsVesselTypeDataAccess.UpdateVesselType(this.VesselTypeId, this.VesselMaxCapacity, this.VesselTypeName, this.VesselTypeDescription, this.HasCabins);
        }

        public bool Save()
        {
            switch (_mode)
            {

                case enMode.enAddNewVesselType:
                    if (this._AddNewVesselType())
                    {
                        _mode = enMode.enUpdateVesselType;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateVesselType:
                    return this._UpdateVesselType();

            }
            return false; // Default case, should not be reached if all modes are handled properly.

        }

        public static bool DeleteVesselType(int vesselTypeId)
        {
            return clsVesselTypeDataAccess.DeleteVesselType(vesselTypeId);
        }

        public static bool IsVesselTypeIdExists(int vesselTypeId)
        {
            return clsVesselTypeDataAccess.IsVesselTypeExists(vesselTypeId);
        }



    }
}
