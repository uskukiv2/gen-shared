using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.process.Infrastructure;

namespace alms.cherry.data.process.Interfaces
{
    public interface IEmployeeProfileRepository : IRepository<EmployeeProfile>
    {
        Task<EmployeeProfile> GetProfileFromEmployeeId(Guid employeeId);
    }
}
