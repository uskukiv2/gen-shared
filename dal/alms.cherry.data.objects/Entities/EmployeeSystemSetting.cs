using System;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities
{
    public class EmployeeSystemSetting : IGuidable, IActivable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SettingBody { get; set; }
        public Guid EmployeeId { get; set; }
        public bool Active { get; set; }

        public Employee Employee { get; set; }
    }
}
