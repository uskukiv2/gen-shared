using System.Collections.Generic;
using System.Threading.Tasks;
using alms.cherry.data.objects.Entities.Product;
using alms.cherry.data.process.Infrastructure;

namespace alms.cherry.data.process.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
    }
}