using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities;
using alms.cherry.data.objects.Entities.Employees;
using FluentMigrator;

namespace alms.application.installer.library.Migrations
{
    [Migration(20210122175042)]
    public class AddEmployeeSystemSettings : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.EmployeeSystemSettingTable)
                .WithColumn(nameof(EmployeeSystemSetting.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(EmployeeSystemSetting.Name)).AsString().NotNullable()
                .WithColumn(nameof(EmployeeSystemSetting.SettingBody)).AsString().NotNullable()
                .WithColumn(nameof(EmployeeSystemSetting.Active)).AsBoolean().NotNullable()
                .WithColumn(nameof(EmployeeSystemSetting.EmployeeId)).AsGuid()
                .ForeignKey(nameof(EmployeeSystemSetting.EmployeeId), TableNames.EmployeeTable, nameof(Employee.Id));
        }

        public override void Down()
        {
            Delete.Table(TableNames.EmployeeSystemSettingTable);
        }
    }
}
