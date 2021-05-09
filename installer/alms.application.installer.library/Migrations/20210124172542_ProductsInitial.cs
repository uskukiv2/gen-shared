using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities;
using alms.cherry.data.objects.Entities.Employees;
using alms.cherry.data.objects.Entities.Product;
using FluentMigrator;

namespace alms.application.installer.library.Migrations
{
    [Migration(20210124172542)]
    public class ProductsInitial : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.ProductTable)
                .WithColumn(nameof(Product.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(Product.ProductName)).AsString().NotNullable()
                .WithColumn(nameof(Product.AdditionalInformation)).AsString().Nullable()
                .WithColumn(nameof(Product.Brand)).AsString().NotNullable()
                .WithColumn(nameof(Product.Quantity)).AsInt16().NotNullable()
                .WithColumn(nameof(Product.Cost)).AsDecimal().NotNullable()
                .WithColumn(nameof(Product.Active)).AsBoolean().NotNullable()
                .WithColumn(nameof(Product.Created)).AsDateTime2().NotNullable()
                .WithColumn(nameof(Product.Updated)).AsDateTime2().NotNullable()
                .WithColumn(nameof(Product.Number)).AsString().NotNullable()
                .WithColumn(nameof(Product.Barcode)).AsInt32().NotNullable()
                .WithColumn(nameof(Product.ProductCategoryId)).AsInt32().NotNullable()
                .WithColumn(nameof(Product.ProductTypeId)).AsInt32().NotNullable()
                .WithColumn(nameof(Product.ProductUnitId)).AsInt32().NotNullable()
                .WithColumn(nameof(Product.VatGroupId)).AsGuid().NotNullable();

            Create.Table(TableNames.ProductCategoryTable)
                .WithColumn(nameof(ProductCategory.Id)).AsInt32().PrimaryKey()
                .WithColumn(nameof(ProductCategory.Name)).AsString().NotNullable()
                .WithColumn(nameof(ProductCategory.Description)).AsString().Nullable()
                .WithColumn(nameof(ProductCategory.Created)).AsDateTime2().NotNullable()
                .WithColumn(nameof(ProductCategory.Updated)).AsDateTime2().NotNullable()
                .WithColumn(nameof(ProductCategory.Active)).AsBoolean().NotNullable();

            Create.Table(TableNames.ProductTypeTable)
                .WithColumn(nameof(ProductType.Id)).AsInt32().PrimaryKey()
                .WithColumn(nameof(ProductType.Name)).AsString().NotNullable()
                .WithColumn(nameof(ProductType.CategoryId)).AsInt32().NotNullable()
                .ForeignKey(nameof(ProductType.CategoryId), TableNames.ProductCategoryTable,
                    nameof(ProductCategory.Id))
                .WithColumn(nameof(ProductType.Updated)).AsDateTime2().NotNullable();

            Create.Table(TableNames.ProductUnitTable)
                .WithColumn(nameof(ProductUnit.Id)).AsInt32().PrimaryKey()
                .WithColumn(nameof(ProductUnit.Name)).AsString().NotNullable()
                .WithColumn(nameof(ProductUnit.Updated)).AsDateTime2().NotNullable()
                .WithColumn(nameof(ProductUnit.Active)).AsBoolean().NotNullable();

            Alter.Table(TableNames.ProductTable)
                .AlterColumn(nameof(Product.ProductCategoryId)).AsInt32()
                .ForeignKey(nameof(Product.ProductCategoryId), TableNames.ProductCategoryTable, nameof(ProductCategory.Id))
                .AlterColumn(nameof(Product.ProductTypeId)).AsInt32()
                .ForeignKey(nameof(Product.ProductTypeId), TableNames.ProductTypeTable, nameof(ProductType.Id))
                .AlterColumn(nameof(Product.ProductUnitId)).AsInt32()
                .ForeignKey(nameof(Product.ProductUnitId), TableNames.ProductUnitTable, nameof(ProductUnit.Id));
        }

        public override void Down()
        {
            Delete.Table(TableNames.ProductTable);
            Delete.Table(TableNames.ProductUnitTable);
            Delete.Table(TableNames.ProductTypeTable);
            Delete.Table(TableNames.ProductCategoryTable);
        }
    }
}
