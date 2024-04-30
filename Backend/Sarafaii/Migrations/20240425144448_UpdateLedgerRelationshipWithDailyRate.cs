using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sarafaii.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLedgerRelationshipWithDailyRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ledgers_DailyRate_DailyRateCurrencyId_DailyRateDate_DailyRateUserId",
                table: "Ledgers");

            migrationBuilder.DropIndex(
                name: "IX_Ledgers_DailyRateCurrencyId_DailyRateDate_DailyRateUserId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "DailyRateCurrencyId",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "DailyRateDate",
                table: "Ledgers");

            migrationBuilder.DropColumn(
                name: "DailyRateUserId",
                table: "Ledgers");

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Ledgers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Ledgers");

            migrationBuilder.AddColumn<int>(
                name: "DailyRateCurrencyId",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DailyRateDate",
                table: "Ledgers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DailyRateUserId",
                table: "Ledgers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ledgers_DailyRateCurrencyId_DailyRateDate_DailyRateUserId",
                table: "Ledgers",
                columns: new[] { "DailyRateCurrencyId", "DailyRateDate", "DailyRateUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ledgers_DailyRate_DailyRateCurrencyId_DailyRateDate_DailyRateUserId",
                table: "Ledgers",
                columns: new[] { "DailyRateCurrencyId", "DailyRateDate", "DailyRateUserId" },
                principalTable: "DailyRate",
                principalColumns: new[] { "CurrencyId", "Date", "UserId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
