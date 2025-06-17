using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.OwnersData;

namespace nawras_business_layer.Owners
{
    public class clsOwner
    {

        private enum enMode { enAddOwner = 1, enUpdateOwner = 2 }
        private enMode _Mode;

        public int OwnerId { get; set; }
        public int StandardUserId { get; set; }
        public int PersonId { get; set; }
        public bool IsBusiness { get; set; }
        public int NumberOfAssets { get; set; }
        public DateTime CreatedAt { get; set; }


        public clsOwner()
        {
            OwnerId = -1;
            StandardUserId = -1;
            PersonId = -1;
            IsBusiness = false;
            NumberOfAssets = 0;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddOwner;
        }

        private clsOwner(int ownerId, int standardUserId, int personId, bool isBusiness, int numberOfAssets, DateTime createdAt)
        {
            OwnerId = ownerId;
            StandardUserId = standardUserId;
            PersonId = personId;
            IsBusiness = isBusiness;
            NumberOfAssets = numberOfAssets;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdateOwner;
        }

        public static clsOwner FindOwnerById(int ownerId)
        {
            int standardUser = -1;
            int personId = -1;
            bool isBusiness = false;
            int numberOfAssets = 0;
            DateTime createdAt = DateTime.Now;

            if (clsOwnerDataAccess.GetOwnerInfoById(ownerId, ref standardUser, ref personId, ref isBusiness, ref numberOfAssets, ref createdAt))
            {
                return new clsOwner(ownerId, standardUser, personId, isBusiness, numberOfAssets, createdAt);
            }
            else
            {
                return null;
            }
        }

        public static DataTable ListAllOwners()
        {
            return clsOwnerDataAccess.ListAllOwners();
        }

        private bool _AddNewOwner()
        {
            this.OwnerId = clsOwnerDataAccess.AddNewOwner(this.PersonId, this.StandardUserId, this.IsBusiness, this.NumberOfAssets, this.CreatedAt);

            return (this.OwnerId != -1);
        }

        private bool _UpdateOwner()
        {
            return clsOwnerDataAccess.UpdateOwner(this.OwnerId, this.StandardUserId, this.PersonId, this.IsBusiness, this.NumberOfAssets, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddOwner:
                    if (_AddNewOwner())
                    {
                        _Mode = enMode.enUpdateOwner;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.enUpdateOwner:
                    return _UpdateOwner();
            }
            return false;

        }

        public bool DeleteOwner(int ownerId)
        {
            return clsOwnerDataAccess.DeleteOwner(ownerId);
        }

        public bool IsOwnerExist(int ownerId)
        {
            return clsOwnerDataAccess.IsOwnerExist(ownerId);
        }


    }
}
