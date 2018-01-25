using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace dotnetDrinks.Data.Migrations
{
    public partial class CompaniesFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_Company_CompanyId",
                table: "Drink");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Drink",
                newName: "CompanyID");

            migrationBuilder.RenameIndex(
                name: "IX_Drink_CompanyId",
                table: "Drink",
                newName: "IX_Drink_CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_Company_CompanyID",
                table: "Drink",
                column: "CompanyID",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_Company_CompanyID",
                table: "Drink");

            migrationBuilder.RenameColumn(
                name: "CompanyID",
                table: "Drink",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_Drink_CompanyID",
                table: "Drink",
                newName: "IX_Drink_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_Company_CompanyId",
                table: "Drink",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
