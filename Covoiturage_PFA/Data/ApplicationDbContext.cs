using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Covoiturage_PFA.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Trajet> Trajets { get; set; }
    public DbSet<TrajetPassager> TrajetPassagers { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<DemandeProfil> DemandeProfils { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Clé composite sur table de liaison
        modelBuilder.Entity<TrajetPassager>()
            .HasKey(tp => new { tp.TrajetId, tp.UtilisateurId });

        // Relations
        modelBuilder.Entity<TrajetPassager>()
            .HasOne(tp => tp.Trajet)
            .WithMany(t => t.TrajetPassagers)
            .HasForeignKey(tp => tp.TrajetId);

        modelBuilder.Entity<TrajetPassager>()
            .HasOne(tp => tp.Utilisateur)
            .WithMany(u => u.ReservationsPassager)
            .HasForeignKey(tp => tp.UtilisateurId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Trajet>()
            .HasOne(t => t.Conducteur)
            .WithMany(u => u.TrajetsConducteur)
            .HasForeignKey(t => t.ConducteurId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TrajetPassager>()
            .HasOne(tp => tp.Status)
            .WithMany()
            .HasForeignKey(tp => tp.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

       
    }
}
