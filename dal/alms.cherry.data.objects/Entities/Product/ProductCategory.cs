using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Product
{
    public class ProductCategory : IActivable, IDateUpdate, IDateCreate, IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public bool Active { get; set; }

        public int? ParentCategoryId { get; set; }

        public ProductCategory ParentCategory { get; set; }

        public ICollection<Product> Products { get; }

        public ICollection<ProductType> ProductTypes { get; set; }

        public IEnumerable<ProductCategory> ChildCategories { get; set; }

        private ProductCategory()
        {
            Active = true;
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
            Products = new Collection<Product>();
        }

        private ProductCategory(int id, string name, string description) : this()
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public ProductCategory CreateNew(int id, string name, string description)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Value {nameof(id)} lower than zero");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("message", nameof(description));
            }

            return new ProductCategory(id, name, description);
        }
    }
}