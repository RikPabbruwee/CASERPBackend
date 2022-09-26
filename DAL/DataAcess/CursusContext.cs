using DAL.DomainConfigurations;
using Domain_Classes;
using Microsoft.EntityFrameworkCore;

namespace DAL.DataAcess
{
    public class CursusContext : DbContext
    {
        public virtual DbSet<Cursus> Cursussen { get; set; }
        public virtual DbSet<CursusInstantie> CursusInstanties { get; set; }
        public virtual DbSet<Cursist> Cursisten { get; set; }
        public virtual DbSet<FavoriteWeek> FavoriteWeeks { get; set; }
        public CursusContext(DbContextOptions options) : base(options)
        {

        }
        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Cursus>(new CursusConfiguration());
            modelBuilder.ApplyConfiguration<CursusInstantie>( new CursusInstantieConfiguration());
        }
    }
}
