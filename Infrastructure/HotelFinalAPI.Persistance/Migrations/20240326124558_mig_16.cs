using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelFinalAPI.Persistance.Migrations
{
    public partial class mig_16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ReservationId",
                table: "Bills",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Reservations_ReservationId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_ReservationId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Bills");
        }
    }
}
