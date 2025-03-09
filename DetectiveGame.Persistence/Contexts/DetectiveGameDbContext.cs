using DetectiveGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DetectiveGame.Persistence.Contexts
{
    public class DetectiveGameDbContext : DbContext
    {
        public DetectiveGameDbContext(DbContextOptions<DetectiveGameDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Evidence> Evidences { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                
                entity.HasMany(e => e.Players)
                    .WithOne(e => e.Game)
                    .HasForeignKey(e => e.GameId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Evidences)
                    .WithOne(e => e.Game)
                    .HasForeignKey(e => e.GameId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Notes)
                    .WithOne(e => e.Game)
                    .HasForeignKey(e => e.GameId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.ConnectionId).IsRequired();

                entity.HasMany(e => e.Notes)
                    .WithOne(e => e.Player)
                    .HasForeignKey(e => e.PlayerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Evidence>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);
            });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
} 