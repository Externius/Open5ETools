using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Open5ETools.Infrastructure.Migrations.SqliteMigrations
{
    /// <inheritdoc />
    public partial class AddTriggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var tables = new[] { "Users", "Dungeons", "DungeonOptions", "Options", "Monsters", "Treasures", "Spells" };
            foreach (var table in tables)
            {
                migrationBuilder.Sql(
                    @$"
                    CREATE TRIGGER Set{table}TimestampOnUpdate
                    AFTER UPDATE ON {table}
                    BEGIN
                        UPDATE {table}
                        SET Timestamp = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                ");
                migrationBuilder.Sql(
                    @$"
                    CREATE TRIGGER Set{table}TimestampOnInsert
                    AFTER INSERT ON {table}
                    BEGIN
                        UPDATE {table}
                        SET Timestamp = randomblob(8)
                        WHERE rowid = NEW.rowid;
                    END
                    ");
            }

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
