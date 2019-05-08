using StarsWars.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarsWars.Data
{
    public class StarsWarsDbContext : DbContext
    {
        public StarsWarsDbContext() : 
            base("StarsWarsDbContext")
        {
            //this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Episode> Episodes { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>().ToTable("Characters");

            modelBuilder.Entity<Character>()
                .HasKey(e => e.Id)
                .Property(e => e.Id)
                .HasColumnName("CharacterId");

            modelBuilder.Entity<Character>()
                .Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired();

            modelBuilder.Entity<Character>().HasMany<Episode>(u => u.Episodes).WithRequired(u => u.Character).Map(m => m.MapKey("CharacterId"));
            modelBuilder.Entity<Character>().HasMany<Friend>(u => u.Friends).WithRequired(u => u.Character).Map(m => m.MapKey("CharacterId"));

            //modelBuilder.Entity<Episode>().HasRequired(u => u.Character).WithMany().Map(m => m.MapKey("CharacterId"));
            modelBuilder.Entity<Episode>().HasRequired(t => t.Character).WithMany(t => t.Episodes);

            modelBuilder.Entity<Episode>().ToTable("Episodes");

            modelBuilder.Entity<Episode>()
               .HasKey(e => e.Id)
               .Property(e => e.Id)
               .HasColumnName("EpisodeId");

            modelBuilder.Entity<Episode>()
                .Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired();

            //modelBuilder.Entity<Friend>().HasRequired(u => u.Character).WithMany().Map(m => m.MapKey("CharacterId"));
            modelBuilder.Entity<Friend>().HasRequired(t => t.Character).WithMany(t => t.Friends);

            modelBuilder.Entity<Friend>().ToTable("Friends");

            modelBuilder.Entity<Friend>()
               .HasKey(e => e.Id)
               .Property(e => e.Id)
               .HasColumnName("FriendId");

            modelBuilder.Entity<Friend>()
                .Property(e => e.Name)
                .HasColumnName("Name")
                .IsRequired();



        }
    }
}
