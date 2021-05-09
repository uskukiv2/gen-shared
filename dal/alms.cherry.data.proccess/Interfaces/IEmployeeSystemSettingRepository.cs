using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using alms.cherry.data.objects.Entities;
using alms.cherry.data.process.Infrastructure;

namespace alms.cherry.data.process.Interfaces
{
    public interface IEmployeeSystemSettingRepository : IRepository<EmployeeSystemSetting>
    {
        Task<IEnumerable<EmployeeSystemSetting>> GetSettingsByEmployeeId(Guid employeeId);
    }
}
