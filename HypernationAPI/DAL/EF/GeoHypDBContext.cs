using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.EF
{
    public partial class GeoHypDBContext : DbContext
    {
        public GeoHypDBContext()
            : base("name=GeoHypDBContext")
        {
        }

        public virtual DbSet<Barbarism> Barbarisms { get; set; }
        public virtual DbSet<Morphology> Morphologies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barbarism>()
                .Property(e => e.decription)
                .IsUnicode(false);

            modelBuilder.Entity<Morphology>()
                .Property(e => e.correct_word)
                .IsFixedLength();
        }
    }
}
