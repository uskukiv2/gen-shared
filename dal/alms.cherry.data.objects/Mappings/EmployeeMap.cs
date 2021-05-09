using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using alms.cherry.data.objects.Entities;
using alms.cherry.data.objects.Entities.Employees;
using RepoDb;

namespace alms.cherry.data.objects.Mappings
{
    public class EmployeeMap
    {
        public EmployeeMap()
        {
            FluentMapper.Entity<Employee>()
                .Table(TableNames.EmployeeTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.UserName, nameof(Employee.UserName))
                .DbType(x => x.UserName, DbType.String)
                .Column(e => e.Active, nameof(Employee.Active))
                .Column(e => e.Rotation, nameof(Employee.Rotation))
                .Column(e => e.EmployeePermissionId, nameof(Employee.EmployeePermissionId))
                .Column(e => e.EmployeeProfileId, nameof(Employee.EmployeeProfileId));
        }
    }

    public class EmployeeProfileMap
    {
        public EmployeeProfileMap()
        {
            FluentMapper.Entity<EmployeeProfile>()
                .Table(TableNames.EmployeeTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.FirstName, nameof(EmployeeProfile.FirstName))
                .Column(e => e.LastName, nameof(EmployeeProfile.LastName))
                .Column(e => e.EmployeeId, nameof(EmployeeProfile.EmployeeId));
        }
    }

    public class EmployeeSystemSettingsMap
    {
        public EmployeeSystemSettingsMap()
        {
            FluentMapper.Entity<EmployeeSystemSetting>()
                .Table(TableNames.EmployeeSystemSettingTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.Name, nameof(EmployeeSystemSetting.Name))
                .Column(e => e.SettingBody, nameof(EmployeeSystemSetting.SettingBody))
                .Column(e => e.EmployeeId, nameof(EmployeeSystemSetting.EmployeeId))
                .Column(e => e.Active, nameof(EmployeeSystemSetting.Active));
        }
    }
}
