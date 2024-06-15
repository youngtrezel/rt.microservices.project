﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketing.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Registration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numbers = table.Column<int>(type: "int", nullable: false),
                    Reserved = table.Column<bool>(type: "bit", nullable: false),
                    Sold = table.Column<bool>(type: "bit", nullable: false),
                    DateSold = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceSoldFor = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plates");
        }
    }
}
