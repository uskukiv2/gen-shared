using System;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Employees
{
    public class EmployeePermission : IEntity, IGuidable
    {
        public Guid Id { get; }
    }
}
