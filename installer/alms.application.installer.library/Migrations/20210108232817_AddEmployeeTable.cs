using alms.cherry.data.objects;
using alms.cherry.data.objects.Entities.Employees;
using FluentMigrator;

namespace alms.application.installer.library.Migrations
{
    [Migration(20210108232817)]
    public class AddEmployeeTable : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.EmployeeTable)
                .WithColumn(nameof(Employee.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(Employee.UserName)).AsString().NotNullable()
                .WithColumn(nameof(Employee.Rotation)).AsCustom("bytea").NotNullable()
                .WithColumn(nameof(Employee.Active)).AsBoolean().NotNullable()
                .WithColumn(nameof(Employee.EmployeeProfileId)).AsGuid().Nullable()
                .WithColumn(nameof(Employee.EmployeePermissionId)).AsGuid().Nullable();

            Create.Table(TableNames.EmployeeProfileTable)
                .WithColumn(nameof(EmployeeProfile.Id)).AsGuid().PrimaryKey()
                .WithColumn(nameof(EmployeeProfile.FirstName)).AsString().NotNullable()
                .WithColumn(nameof(EmployeeProfile.LastName)).AsString().NotNullable()
                .WithColumn(nameof(EmployeeProfile.Active)).AsBoolean().NotNullable()
                .WithColumn(nameof(EmployeeProfile.EmployeeId)).AsGuid()
                .ForeignKey(nameof(EmployeeProfile.EmployeeId), TableNames.EmployeeTable, nameof(Employee.Id));

            Alter.Table(TableNames.EmployeeTable)
                .AlterColumn(nameof(Employee.EmployeeProfileId)).AsGuid()
                .ForeignKey(nameof(Employee.EmployeeProfileId), TableNames.EmployeeProfileTable,
                    nameof(EmployeeProfile.Id));
        }

        public override void Down()
        {
            Delete.Table(TableNames.EmployeeTable);
            Delete.Table(TableNames.EmployeeProfileTable);
        }
    }
}
