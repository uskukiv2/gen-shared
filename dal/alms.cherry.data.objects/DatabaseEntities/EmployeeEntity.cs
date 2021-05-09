using System;
using System.Collections.Generic;
using System.Text;
using alms.cherry.data.objects.DatabaseEntities.Base;
using alms.cherry.data.objects.Entities.Employees;

namespace alms.cherry.data.objects.DatabaseEntities
{
    public class EmployeeEntity : DbEntity<Employee>
    {
        public EmployeeEntity(string entityName) : base(entityName)
        {
        }
    }
}
