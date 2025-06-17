using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nawras_data_access.EmployeesData;

namespace nawras_business_layer.Employess
{
    public class clsEmployee
    {

        private enum enMode { enAddNewEmployee = 1, enUpdateEmployee = 2 }
        private enMode _Mode = enMode.enAddNewEmployee;


        public int EmployeeId { get; set; }
        public int PersonId { get; set; }
        public decimal Salary { get; set; }
        public string Job { get; set; }
        public DateTime CreatedAt { get; set; }

        public clsEmployee()
        {
            EmployeeId = -1;
            PersonId = -1;
            Salary = 0;
            Job = string.Empty;
            CreatedAt = DateTime.Now;

            _Mode = enMode.enAddNewEmployee;

        }

        public clsEmployee(int employeeId, int personId, decimal salary, string job, DateTime createdAt)
        {
            EmployeeId = employeeId;
            PersonId = personId;
            Salary = salary;
            Job = job;
            CreatedAt = createdAt;

            _Mode = enMode.enUpdateEmployee;

        }


        public static clsEmployee FindEmployeeById(int employeeId)
        {

            int personId = -1;
            decimal salary = 0;
            string job = string.Empty;
            DateTime createdAt = DateTime.Now;

            if (clsEmployeeDataAccess.GetEmployeeInfoById(employeeId, ref personId, ref salary, ref job, ref createdAt))
            {
                return new clsEmployee(employeeId, personId, salary, job, createdAt);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewEmployee()
        {
            this.EmployeeId = clsEmployeeDataAccess.AddNewEmployee(this.PersonId, this.Salary, this.Job, this.CreatedAt);

            return (EmployeeId != -1);
        }

        private bool _UpdateTenant()
        {
            return clsEmployeeDataAccess.UpdateEmployee(this.EmployeeId, this.PersonId, this.Salary, this.Job, this.CreatedAt);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.enAddNewEmployee:
                    if (_AddNewEmployee())
                    {
                        _Mode = enMode.enUpdateEmployee;
                        return true;
                    }
                    else
                    { return false; }
                case enMode.enUpdateEmployee:
                    return _UpdateTenant();

            }

            return false;
        }

        public static bool IsEmployeeExist(int employeeId)
        {
            return clsEmployeeDataAccess.IsEmployeeExist(employeeId);
        }

        public static bool DeleteEmployee(int employeeId)
        {
            return clsEmployeeDataAccess.DeleteEmployee(employeeId);
        }

        public static DataTable ListAllEmployees()
        {
            return clsEmployeeDataAccess.ListAllEmployees();
        }


    }




















}
