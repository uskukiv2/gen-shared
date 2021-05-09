using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities.Product;
using FluentMigrator;

namespace alms.application.installer.library.Migrations
{
    [Migration(20210131230155)]
    public class UpdateCategoryv1 : Migration
    {
        public override void Up()
        {
            Alter.Table(TableNames.ProductCategoryTable)
                .AddColumn(nameof(ProductCategory.ParentCategoryId)).AsInt32().Nullable().ForeignKey(
                    nameof(ProductCategory.ParentCategoryId), TableNames.ProductCategoryTable,
                    nameof(ProductCategory.Id));
        }

        public override void Down()
        {
            Delete.Column(nameof(ProductCategory.ParentCategoryId)).FromTable(TableNames.ProductCategoryTable);
        }
    }
}
