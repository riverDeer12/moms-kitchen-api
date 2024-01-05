using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MomsKitchen.Migrations
{
    /// <inheritdoc />
    public partial class Init01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: "cd75a482-cac0-45f2-9c20-bae54f363742",
                columns: new[] { "ActivityUpdatedAt", "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 4, 20, 41, 19, 354, DateTimeKind.Utc).AddTicks(9398), "8cc23211-b560-4d99-bc17-feef9c5e3909", new DateTime(2024, 1, 4, 20, 41, 19, 354, DateTimeKind.Utc).AddTicks(9395), "AQAAAAIAAYagAAAAEPUzq0+xfnJXR0bqUVBvKh6sK3SPWLTxDWag6yCMlIfSbQOyq8UiJkGFNfVc/V63JA==", "183cacd8-5657-4e1c-b2e7-40f1649f8012", new DateTime(2024, 1, 4, 20, 41, 19, 354, DateTimeKind.Utc).AddTicks(9398) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: "cd75a482-cac0-45f2-9c20-bae54f363742",
                columns: new[] { "ActivityUpdatedAt", "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 4, 13, 43, 28, 469, DateTimeKind.Utc).AddTicks(3262), "bdf2fae7-2bb7-473c-80bf-5baa8f1bcbda", new DateTime(2024, 1, 4, 13, 43, 28, 469, DateTimeKind.Utc).AddTicks(3257), "AQAAAAIAAYagAAAAEEf4Qp4bTtsDoq+tLe2RmoNhpAYW7v6eWxqgZFxCFyeqyTZba4kJVEYYUcwRgwtOKQ==", "7e6a6050-b5d0-4a37-86d6-d8ebdf3959ba", new DateTime(2024, 1, 4, 13, 43, 28, 469, DateTimeKind.Utc).AddTicks(3260) });
        }
    }
}
