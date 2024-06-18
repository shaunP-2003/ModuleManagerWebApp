using Microsoft.EntityFrameworkCore;
using PROG_POE.Models.Domain;
using System.Reflection;

namespace PROG_POE.Data
{
    public class ModuleAppDbContext: DbContext
    {
        public ModuleAppDbContext(DbContextOptions<ModuleAppDbContext> options) : base(options)
        {

        }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<StudyHoursRecords> StudyHoursRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Modules>()
            .HasOne(m => m.User)
            .WithMany(u => u.Modules)
            .HasForeignKey(m => m.UserId)
            .IsRequired();
            

            // StudentRecordHours
            modelBuilder.Entity<StudyHoursRecords>()
            .HasKey(sr => sr.StudyRecordId);

            // Configure the relationship between StudyRecord and Module
            modelBuilder.Entity<StudyHoursRecords>()
                   .HasOne(sr => sr.Modules)
                   .WithMany(m => m.StudyHoursRecords)
                   .HasForeignKey(sr => sr.ModuleId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

            // Configure the relationship between StudyRecord and User
            modelBuilder.Entity<StudyHoursRecords>()
                .HasOne(sr => sr.User)
                .WithMany(u => u.StudyHoursRecords)
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}

