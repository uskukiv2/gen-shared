using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.objects.Entities.Product;
using RepoDb;

namespace alms.cherry.data.objects.Mappings
{
    public class ProductsMap
    {
        public ProductsMap()
        {
            FluentMapper.Entity<Product>()
                .Table(TableNames.ProductTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.ProductName, nameof(Product.ProductName))
                .Column(e => e.Brand, nameof(Product.Brand))
                .Column(e => e.Active, nameof(Product.Active))
                .Column(e => e.Cost, nameof(Product.Cost))
                .Column(e => e.AdditionalInformation, nameof(Product.AdditionalInformation))
                .Column(e => e.ProductCategoryId, nameof(Product.ProductCategoryId))
                .Column(e => e.ProductTypeId, nameof(Product.ProductTypeId))
                .Column(e => e.VatGroupId, nameof(Product.VatGroupId))
                .Column(e => e.ProductUnitId, nameof(Product.ProductUnitId))
                .Column(e => e.Quantity, nameof(Product.Quantity))
                .Column(e => e.Number, nameof(Product.Number))
                .Column(e => e.Barcode, nameof(Product.Barcode))
                .Column(e => e.Created, nameof(Product.Created))
                .Column(e => e.Updated, nameof(Product.Updated));
        }
    }

    public class ProductTypeMap
    {
        public ProductTypeMap()
        {
            FluentMapper.Entity<ProductType>()
                .Table(TableNames.ProductTypeTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.Name, nameof(ProductType.Name))
                .Column(e => e.Updated, nameof(ProductType.Updated))
                .Column(e => e.CategoryId, nameof(ProductType.CategoryId));
        }
    }

    public class ProductCategoryMap
    {
        public ProductCategoryMap()
        {
            FluentMapper.Entity<ProductCategory>()
                .Table(TableNames.ProductCategoryTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.Name, nameof(ProductCategory.Name))
                .Column(e => e.Description, nameof(ProductCategory.Description))
                .Column(e => e.Created, nameof(ProductCategory.Created))
                .Column(e => e.Updated, nameof(ProductCategory.Updated))
                .Column(e => e.Active, nameof(ProductCategory.Active));
        }
    }

    public class ProductUnitMap
    {
        public ProductUnitMap()
        {
            FluentMapper.Entity<ProductUnit>()
                .Table(TableNames.ProductUnitTable)
                .Primary(e => e.Id)
                .Identity(e => e.Id)
                .Column(e => e.Name, nameof(ProductCategory.Name))
                .Column(e => e.Updated, nameof(ProductCategory.Updated))
                .Column(e => e.Active, nameof(ProductCategory.Active));
        }
    }
}
