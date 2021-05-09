using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Product
{
    public class ProductType : IDateUpdate, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public DateTime Updated { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public ICollection<Product> Products { get; set; }

        private ProductType()
        {
            Updated = DateTime.UtcNow;
            Products = new Collection<Product>();
        }

        private ProductType(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public ProductType CreateNew(int id, string name)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("message", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            return new ProductType(id, name);
        }
    }
}