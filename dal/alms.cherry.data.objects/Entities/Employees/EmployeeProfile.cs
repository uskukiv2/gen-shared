using System;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Employees
{
    /// <summary>
    /// Employee Profile
    /// <param name="FirstName">Locate first name of user</param>
    /// <param name="LastName">locate last name of user</param>
    /// <param name="ByteName">Locate image of user</param>
    /// <param name="User">Locate rotation and username of user</param>
    /// </summary>
    public class EmployeeProfile : IGuidable, IActivable, IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public byte[] ByteImage { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public bool Active { get; set; }
    }
}