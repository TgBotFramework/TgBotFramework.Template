using Microsoft.EntityFrameworkCore;

namespace TgBotFramework.Template.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<ChatStage> Stages { get; set; }
}