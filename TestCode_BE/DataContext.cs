using System.Collections.Generic;
using TestCode_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace TestCode_BE
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }
        public DbSet<Bpkb> Bpkbs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("ms_user");
            modelBuilder.Entity<StorageLocation>().ToTable("ms_storage_location");
            modelBuilder.Entity<Bpkb>().ToTable("tr_bpkb");
        }
    }
}
