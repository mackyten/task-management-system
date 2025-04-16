using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMS.INFRASTRUCTURE.Migrations
{
    /// <inheritdoc />
    public partial class _00000006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualTimeSpent",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Tasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFinished",
                table: "Tasks",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateFinished",
                table: "Tasks");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ActualTimeSpent",
                table: "Tasks",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EstimatedTime",
                table: "Tasks",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
