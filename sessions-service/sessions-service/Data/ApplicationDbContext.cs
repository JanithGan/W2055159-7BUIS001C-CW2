using Microsoft.EntityFrameworkCore;
using SessionsService.Models;

namespace SessionsService.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<StudySession> StudySessions { get; set; }
    public DbSet<Break> Breaks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}