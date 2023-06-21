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
    }
}
