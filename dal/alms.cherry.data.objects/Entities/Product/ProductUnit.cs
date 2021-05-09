using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Product
{
    public class ProductUnit : IDateUpdate, IActivable, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Updated { get; set; }

        public ICollection<Product> Products { get; set; }

        public bool Active { get; set; }

        private ProductUnit()
        {
            Updated = DateTime.UtcNow;
            Products = new Collection<Product>();
            Active = true;
        }

        private ProductUnit(int id, string name) : this()
        {
            Id = id;
            Name = name;
        }

        public ProductUnit CreateNew(int id, string name) 
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("message", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            return new ProductUnit(id, name);
        }
    }
}