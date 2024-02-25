using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace IplPlayersClubDetailsApp.Models;

public partial class IplManagementContext : DbContext
{
    public IplManagementContext()
    {
    }

    public IplManagementContext(DbContextOptions<IplManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    #region LazyLoadng
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
            var connectionString = configuration.GetConnectionString("PlayerManagement");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
    #endregion

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Club>(entity =>
    //    {
    //        entity.HasKey(e => e.ClubId).HasName("PK__Clubs__D35058E7E9F679DC");

    //        entity.Property(e => e.Name).HasMaxLength(30);
    //    });

    //    modelBuilder.Entity<Player>(entity =>
    //    {
    //        entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74C88F56CAA7");

    //        entity.Property(e => e.DateOfJoining).HasColumnType("smalldatetime");
    //        entity.Property(e => e.FirstName).HasMaxLength(20);
    //        entity.Property(e => e.LastName).HasMaxLength(20);

    //        entity.HasOne(d => d.Club).WithMany(p => p.Players)
    //            .HasForeignKey(d => d.ClubId)
    //            .OnDelete(DeleteBehavior.SetNull)
    //            .HasConstraintName("FK__Players__ClubId__440B1D61");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

