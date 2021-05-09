using System.Threading.Tasks;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.process.Infrastructure;

namespace alms.cherry.data.process.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetEmployeWithRotationAndUsername(string username, byte[] rotation);
    }
}
