using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotDesk.Api.Persistence.HotDesk.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    add_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    remove_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "desks",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    add_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    remove_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    start_reservation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_reservation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    location_id = table.Column<int>(type: "integer", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desks", x => x.id);
                    table.ForeignKey(
                        name: "FK_desks_locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_desks_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    last_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    desk_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "FK_employees_desks_desk_id",
                        column: x => x.desk_id,
                        principalSchema: "public",
                        principalTable: "desks",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "employee_roles",
                schema: "public",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "integer", nullable: false),
                    RolesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_roles", x => new { x.EmployeesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_employee_roles_employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalSchema: "public",
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_roles_roles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_desks_location_id",
                schema: "public",
                table: "desks",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_desks_LocationId",
                schema: "public",
                table: "desks",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_roles_RolesId",
                schema: "public",
                table: "employee_roles",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_employees_desk_id",
                schema: "public",
                table: "employees",
                column: "desk_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee_roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "employees",
                schema: "public");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "desks",
                schema: "public");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "public");
        }
    }
}
