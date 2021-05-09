using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using alms.cherry.common;
using alms.cherry.data.objects.Entities.Employees;

namespace alms.application.installer.library.Managers
{
    public class UserManager
    {
        public UserManager()
        {
            
        }

        public Employee GenerateEmployee(string password)
        {
            var secureString = new SecureString();
            foreach (var c in password.ToCharArray())
            {
                secureString.AppendChar(c);
            }
            var employee =  new Employee
            {
                Id = Guid.NewGuid(),
                UserName = "cherryadmin",
                Rotation = Cryptograph.HashPassword(secureString),
                Active = true,
                EmployeePermissionId = null,
                EmployeeProfileId = null
            };

            return employee;
        }

        public EmployeeProfile GenerateEmployeeProfile(Employee employee, string firstName, string lastName)
        {
            return new EmployeeProfile
            {
                Id = Guid.NewGuid(),
                Active = true,
                Employee = employee,
                EmployeeId = employee.Id,
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
