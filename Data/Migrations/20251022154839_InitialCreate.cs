using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelBooking.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Phone = table.Column<long>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Inn = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    RegistrationAddress = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Region = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                });

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
                name: "TransportingMeans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportingMeans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Passport = table.Column<long>(type: "INTEGER", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Surname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Lastname = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
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
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Payment = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Price = table.Column<float>(type: "REAL", nullable: false),
                    TransportationId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
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
                name: "IX_Books_PassengerId",
                table: "Books",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_StatusId",
                table: "Books",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_TransportationId",
                table: "Books",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_AccountId",
                table: "Passengers",
                column: "AccountId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "TransportationMeans");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "TransportingMeans");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Points");
        }
    }
}
