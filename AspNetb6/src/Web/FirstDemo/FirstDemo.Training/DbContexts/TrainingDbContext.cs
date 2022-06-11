using FirstDemo.Training.Entities;
using FirstDemo.Training.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstDemo.Training.DbContexts
{
    public class TrainingDbContext : DbContext, ITrainingDbContext
    {
        private readonly string _connectionString;
        private readonly string _assemblyName;

        public TrainingDbContext(string connectionString, string assemblyName)
        {
            _assemblyName = assemblyName;
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_assemblyName));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>().HasKey(c => new { c.CourseId, c.StudentId });

            modelBuilder.Entity<Course>()
                .HasMany(n => n.Topics)
                .WithOne(a => a.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(a => a.Course)
                .WithMany(n => n.Students)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(a => a.Student)
                .WithMany(n => n.Courses)
                .HasForeignKey(x => x.StudentId);

            modelBuilder.Entity<Student>()
                .HasData(new StudentSeed().Students);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
    }
}
