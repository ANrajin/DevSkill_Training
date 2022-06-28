using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Training.Entities;

namespace MyApp.Training.DbContexts
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

            //call the base method again
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>().HasKey(c => new { c.CourseId, c.StudentId});

            modelBuilder.Entity<Course>()
                .HasMany(t => t.Topics)
                .WithOne(a => a.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(a => a.Course)
                .WithMany(s => s.Students)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(s => s.Student)
                .WithMany(c => c.Courses)
                .HasForeignKey(x => x.StudentId);

            base.OnModelCreating(modelBuilder);
        }

        //DbSet<Entity> TableName {get; set;}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }
    }
}
