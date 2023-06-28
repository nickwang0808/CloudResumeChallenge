using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Main.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Counters",
                columns: new[] { "Id", "Count" },
                values: new object[] { Guid.NewGuid(), 0 });

            migrationBuilder.Sql(
                @"
                CREATE OR REPLACE PROCEDURE public.incrementcounter(val INTEGER)
                LANGUAGE plpgsql
                AS $$
                BEGIN
                    UPDATE ""Counters""
                    SET ""Count"" = ""Count"" + val;
                END;
                $$;
                "
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counters");
        }
    }
}
