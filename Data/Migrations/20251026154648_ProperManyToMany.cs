using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProperManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Status_StatusId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Transportations_TransportationId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "TransportationMeans");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransportingMeans");

            migrationBuilder.AddColumn<int>(
                name: "TransportationId",
                table: "TransportingMeans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransportingMeanId",
                table: "TransportingMeans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportMeans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportMeans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Departure = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Arrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeparturePointId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArrivalPointId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    PlaceCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    FreePlaceCount = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transports_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Points_ArrivalPointId",
                        column: x => x.ArrivalPointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Points_DeparturePointId",
                        column: x => x.DeparturePointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportingMeans_TransportationId",
                table: "TransportingMeans",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportingMeans_TransportingMeanId",
                table: "TransportingMeans",
                column: "TransportingMeanId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ArrivalPointId",
                table: "Transports",
                column: "ArrivalPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_CompanyId",
                table: "Transports",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_DeparturePointId",
                table: "Transports",
                column: "DeparturePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatus_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "BookStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Transports_TransportationId",
                table: "Books",
                column: "TransportationId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportingMeans_TransportMeans_TransportingMeanId",
                table: "TransportingMeans",
                column: "TransportingMeanId",
                principalTable: "TransportMeans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportingMeans_Transports_TransportationId",
                table: "TransportingMeans",
                column: "TransportationId",
                principalTable: "Transports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatus_StatusId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Transports_TransportationId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportingMeans_TransportMeans_TransportingMeanId",
                table: "TransportingMeans");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportingMeans_Transports_TransportationId",
                table: "TransportingMeans");

            migrationBuilder.DropTable(
                name: "BookStatus");

            migrationBuilder.DropTable(
                name: "TransportMeans");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_TransportingMeans_TransportationId",
                table: "TransportingMeans");

            migrationBuilder.DropIndex(
                name: "IX_TransportingMeans_TransportingMeanId",
                table: "TransportingMeans");

            migrationBuilder.DropColumn(
                name: "TransportationId",
                table: "TransportingMeans");

            migrationBuilder.DropColumn(
                name: "TransportingMeanId",
                table: "TransportingMeans");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransportingMeans",
                type: "TEXT",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ArrivalPointId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    DeparturePointId = table.Column<int>(type: "INTEGER", nullable: false),
                    Arrival = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Departure = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FreePlaceCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    PlaceCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportations_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_Points_ArrivalPointId",
                        column: x => x.ArrivalPointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_Points_DeparturePointId",
                        column: x => x.DeparturePointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportationMeans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Mean = table.Column<int>(type: "INTEGER", nullable: false),
                    Transport = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationMeans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationMeans_Transportations_Transport",
                        column: x => x.Transport,
                        principalTable: "Transportations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportationMeans_TransportingMeans_Mean",
                        column: x => x.Mean,
                        principalTable: "TransportingMeans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationMeans_Mean",
                table: "TransportationMeans",
                column: "Mean");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationMeans_Transport",
                table: "TransportationMeans",
                column: "Transport");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_ArrivalPointId",
                table: "Transportations",
                column: "ArrivalPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_CompanyId",
                table: "Transportations",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_DeparturePointId",
                table: "Transportations",
                column: "DeparturePointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Status_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Transportations_TransportationId",
                table: "Books",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
