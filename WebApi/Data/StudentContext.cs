using Microsoft.EntityFrameworkCore;
using Shared;

namespace WebApi.Data;

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../WebApi/Data/Students.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(student => student.Id);
        modelBuilder.Entity<Grade>().HasKey(grade => grade.Id);
    }
}