using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CA.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRowVersionToGuidConcurrencyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RowVersion",
                table: "Litters",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RowVersion",
                table: "Benefits",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldRowVersion: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Litters",
                type: "BLOB",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RowVersion",
                table: "Benefits",
                type: "BLOB",
                rowVersion: true,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");
        }
    }
}
