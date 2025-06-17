using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_business_layer.Employess;
using nawras_data_access.EmployeesData;
using nawras_data_access.StandardUsersData;

namespace nawras_business_layer
{
    internal class clsStandardUser
    {


        private enum enMode { enAddNewSystemUser = 1, enUpdateSystemUser }
        private enMode _Mode = enMode.enAddNewSystemUser;

        public int StandardUserId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permession { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsStandardUser()
        {
            StandardUserId = -1;
            EmployeeId = -1;
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            Permession = 0;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddNewSystemUser;
        }

        public clsStandardUser(int standardUserId, int personId, string userName, string password, bool isActive, int permession, DateTime createdAt)
        {
            StandardUserId = standardUserId;
            EmployeeId = personId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Permession = permession;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdateSystemUser;
        }

        public static clsStandardUser FindStandardUserById(int standardUserId)
        {
            int employeeId = -1;
            string userName = string.Empty;
            string password = string.Empty;
            bool isActive = false;
            int permession = 0;
            DateTime createdAt = DateTime.Now;

            if (clsStandardUserDataAccess.GetStandardUserInfoById(standardUserId, ref employeeId, ref userName, ref password, ref isActive, ref permession, ref createdAt))
            {

                return new clsStandardUser(standardUserId, employeeId, userName, password, isActive, permession, createdAt);
            }
            else
            {
                return null;
            }

        }

        private bool AddNewStandardUser()
        {
            this.StandardUserId = clsStandardUserDataAccess.AddNewStandardUser(this.EmployeeId, this.UserName, this.Password, this.Permession, this.IsActive, this.CreatedAt);

            return (this.StandardUserId != -1);
        }

        private bool UpdateStandardUser()
        {
            return clsStandardUserDataAccess.UpdateStandardUser(this.StandardUserId, this.EmployeeId, this.UserName, this.Password, this.Permession, this.IsActive, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNewSystemUser:
                    if (AddNewStandardUser())
                    {
                        _Mode = enMode.enUpdateSystemUser;
                        return true;
                    }
                    else
                    { return false; }

                case enMode.enUpdateSystemUser:
                    return UpdateStandardUser();
            }

            return false;
        }

        public static bool DeleteStandardUser(int standardUserId)
        {
            return clsStandardUserDataAccess.DeleteStandardUser(standardUserId);
        }

        public static bool isStandardUserExist(int standardUserId)
        {
            return clsStandardUserDataAccess.isExist(standardUserId);
        }

        public static bool IsStandardUserActive(int standardUserId)
        {
            return clsStandardUserDataAccess.IsActive(standardUserId);
        }







































    }
}
