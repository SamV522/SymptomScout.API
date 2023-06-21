using Microsoft.EntityFrameworkCore;
using SymptomScout.Shared.Models;

namespace SymptomScout.API.Persistence
{
    public class SymptomScoutDbContext : DbContext
    {
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<DiagnosisSymptom> DiagnosisSymptom { get; set; }

        public SymptomScoutDbContext(DbContextOptions<SymptomScoutDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DiagnosisSymptom>(entity =>
            {
                entity.HasKey(ds => new { ds.DiagnosisId, ds.SymptomId });

                entity
                    .HasOne(ds => ds.Diagnosis)
                    .WithMany(d => d.DiagnosisSymptoms)
                    .HasForeignKey(ds => ds.DiagnosisId);

                entity
                    .HasOne(ds => ds.Symptom)
                    .WithMany(s => s.DiagnosisSymptoms)
                    .HasForeignKey(ds => ds.SymptomId);

                entity.HasIndex(ds => new { ds.DiagnosisId, ds.SymptomId }).IsUnique();
            });
        }
    }
}
