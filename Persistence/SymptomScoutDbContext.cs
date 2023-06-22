using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SymptomScout.Shared.Domain;

namespace SymptomScout.API.Persistence
{
    public class SymptomScoutDbContext : DbContext
    {
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }

        public SymptomScoutDbContext(DbContextOptions<SymptomScoutDbContext> options)
            : base(options)
        {

        }
    }
}
