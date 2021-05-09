using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Employees
{
    public class Employee : IActivable, IGuidable, IEntity
    {
        [EntityId]
        public Guid Id { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public byte[] Rotation { get; set; }

        [Required]
        public bool Active { get; set; }

        public Guid? EmployeeProfileId { get; set; }

        public Guid? EmployeePermissionId { get; set; }

        public EmployeeProfile EmployeeProfile { get; set; }

        public EmployeePermission EmployeePermission { get; set; }

        public IEnumerable<EmployeeSystemSetting> EmployeeSystemSettings { get; set; }
    }
}