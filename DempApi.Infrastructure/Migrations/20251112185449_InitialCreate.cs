using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DempApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertedById = table.Column<int>(type: "int", nullable: false),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    InsertedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Deleted", "Description", "InsertedById", "InsertedDate", "Name", "UpdatedById", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, false, "Information Technology", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6679), "IT", null, null },
                    { 2, false, "Human Resources", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6681), "HR", null, null },
                    { 3, false, "Finance Department", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6682), "Finance", null, null },
                    { 4, true, "Marketing Department (Deleted)", 1, new DateTime(2025, 10, 13, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6684), "Marketing", 1, new DateTime(2025, 11, 11, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6695) }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Deleted", "Description", "InsertedById", "InsertedDate", "Title", "UpdatedById", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, false, "Develops software", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6864), "Software Engineer", null, null },
                    { 2, false, "Senior developer", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6866), "Senior Software Engineer", null, null },
                    { 3, false, "Manages HR", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6868), "HR Manager", null, null },
                    { 4, false, "Manages finances", 1, new DateTime(2025, 11, 12, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6869), "Accountant", null, null },
                    { 5, true, "Manages marketing (Deleted)", 1, new DateTime(2025, 10, 13, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6870), "Marketing Manager", 1, new DateTime(2025, 11, 10, 18, 54, 44, 732, DateTimeKind.Utc).AddTicks(6871) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
