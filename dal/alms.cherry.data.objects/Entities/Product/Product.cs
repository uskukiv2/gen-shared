using System;
using alms.cherry.data.objects.Infrastructure;

namespace alms.cherry.data.objects.Entities.Product
{
    public class Product : IActivable, IDateUpdate, IDateCreate, IEntity, IGuidable
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public string Brand { get; set; }

        public decimal Cost { get; set; }

        public string AdditionalInformation { get; set; }

        public int ProductCategoryId { get; set; }

        public int ProductTypeId { get; set; }
        
        public Guid VatGroupId { get; set; }

        public int ProductUnitId { get; set; }

        public decimal Quantity { get; set; }

        public bool Active { get; set; }

        public string Number { get; set; }

        public int Barcode { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public ProductType ProductType { get; set; }

        public VatGroup Vat { get; set; }

        public ProductUnit ProductUnit { get; set; }

        private Product()
        {
            Active = true;
            Created = DateTime.UtcNow;
            Updated = DateTime.UtcNow;
        }

        public Product(Guid id, string name, int categoryId, int typeId)
        {
            Id = id;
            ProductName = name;
            ProductCategoryId = categoryId;
            ProductTypeId = typeId;
        }

        public Product CreateNew(Guid id, string name, int categoryId, int typeId)
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException("message", nameof(id));
            }

            if (categoryId < 0)
            {
                throw new ArgumentOutOfRangeException("message", nameof(categoryId));
            }

            if (typeId < 0)
            {
                throw new ArgumentOutOfRangeException("message", nameof(typeId));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            return new Product(id, name, categoryId, typeId);
        }
    }
}