using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GraphQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "ContactNumber", "CreatedAt", "CreatedBy", "Email", "FirstName", "FullName", "LastName", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[,]
                {
                    { 1, "123 Main St, New York, NY", "+1-555-0101", new DateTime(2026, 1, 20, 5, 31, 17, 171, DateTimeKind.Utc).AddTicks(9139), "System", "john.doe@company.com", "John", "John Doe", "Doe", null, null, "john.doe" },
                    { 2, "456 Oak Ave, Los Angeles, CA", "+1-555-0102", new DateTime(2026, 1, 20, 5, 31, 17, 171, DateTimeKind.Utc).AddTicks(9143), "System", "jane.smith@company.com", "Jane", "Jane Smith", "Smith", null, null, "jane.smith" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "ContactNumber", "CreatedAt", "CreatedBy", "Department", "Email", "EmployeeCode", "FirstName", "FullName", "HireDate", "IsActive", "LastName", "ManagerId", "Position", "Salary", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "789 Pine Rd, San Francisco, CA", "+1-555-0201", new DateTime(2026, 1, 20, 5, 31, 17, 171, DateTimeKind.Utc).AddTicks(9265), "System", "Engineering", "alice.johnson@company.com", "EMP001", "Alice", "Alice Johnson", new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Johnson", 1, "Senior Developer", 95000m, null, null },
                    { 2, "321 Elm St, Seattle, WA", "+1-555-0202", new DateTime(2026, 1, 20, 5, 31, 17, 171, DateTimeKind.Utc).AddTicks(9295), "System", "Engineering", "bob.wilson@company.com", "EMP002", "Bob", "Bob Wilson", new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Wilson", 1, "Junior Developer", 65000m, null, null },
                    { 3, "654 Maple Dr, Boston, MA", "+1-555-0203", new DateTime(2026, 1, 20, 5, 31, 17, 171, DateTimeKind.Utc).AddTicks(9298), "System", "Marketing", "carol.martinez@company.com", "EMP003", "Carol", "Carol Martinez", new DateTime(2021, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Martinez", 2, "Marketing Manager", 85000m, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
