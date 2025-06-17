using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.EmployeesData;

namespace nawras_business_layer.Employess
{
    public class clsSystemUser
    {
        private enum enMode { enAddNewSystemUser = 1, enUpdateSystemUser }
        private enMode _Mode = enMode.enAddNewSystemUser;

        public int SystemUserId { get; set; }
        public int EmployeeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Permession { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsSystemUser()
        {
            SystemUserId = -1;
            EmployeeId = -1;
            UserName = string.Empty;
            Password = string.Empty;
            IsActive = false;
            Permession = 0;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddNewSystemUser;
        }

        public clsSystemUser(int systemUserId, int personId, string userName, string password, bool isActive, int permession, DateTime createdAt)
        {
            SystemUserId = systemUserId;
            EmployeeId = personId;
            UserName = userName;
            Password = password;
            IsActive = isActive;
            Permession = permession;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdateSystemUser;
        }


        public static clsSystemUser FindSystemUserById(int systemUserId)
        {
            int employeeId = -1;
            string userName = string.Empty;
            string password = string.Empty;
            bool isActive = false;
            int permession = 0;
            DateTime createdAt = DateTime.Now;

            if (clsSystemUserDataAccess.GetSystemUserInfoById(systemUserId, ref employeeId, ref userName, ref password, ref isActive, ref permession, ref createdAt))
            {

                return new clsSystemUser(systemUserId, employeeId, userName, password, isActive, permession, createdAt);
            }
            else
            {
                return null;
            }

        }

        private bool _AddNewSystemUser()
        {
            this.SystemUserId = clsSystemUserDataAccess.AddNewSystemUser(this.EmployeeId, this.UserName, this.Password, this.Permession, this.IsActive, this.CreatedAt);

            return (this.SystemUserId != -1);
        }

        private bool _UpdateSystemUser()
        {
            return clsSystemUserDataAccess.UpdateSystemUser(this.SystemUserId, this.EmployeeId, this.UserName, this.Password, this.Permession, this.IsActive, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNewSystemUser:
                    if (_AddNewSystemUser())
                    {
                        _Mode = enMode.enUpdateSystemUser;
                        return true;
                    }
                    else
                    { return false; }

                case enMode.enUpdateSystemUser:
                    return _UpdateSystemUser();
            }

            return false;
        }

        public static bool DeleteSystemUser(int systemUserId)
        {
            return clsSystemUserDataAccess.DeleteSystemUser(systemUserId);
        }

        public static bool isSystemUserExist(int systemUserId)
        {
            return clsSystemUserDataAccess.isExist(systemUserId);
        }

        public static bool isSystemUserActive(int systemUserId)
        {
            return clsSystemUserDataAccess.IsActive(systemUserId);
        }
    }


}
