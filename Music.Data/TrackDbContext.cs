using Microsoft.EntityFrameworkCore;
using Music.Core.Model;
using Music.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Data
{
    public class TrackDbContext : DbContext
    {
        public TrackDbContext(DbContextOptions<TrackDbContext> options) : base(options)
        {
        }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(TrackDbContext).Assembly);
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new TrackConfiguration());
        }
    }
}
