using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;
public class SqliteDbContext: DbContext
{
    public DbSet<AttemptEntity> Attempts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder x)
        => x.UseSqlite(@"DataSource=Data\\SqliteDatabase.db");
}
