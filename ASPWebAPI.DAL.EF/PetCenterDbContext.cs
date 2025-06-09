
using ASPWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.DAL.EF;

public partial class PetCenterDbContext : DbContext
{
    public PetCenterDbContext()
    {
    }

    public PetCenterDbContext(DbContextOptions<PetCenterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adopter> Adopters { get; set; }

    public virtual DbSet<AdoptionRequest> AdoptionRequests { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Volunteer> Volunteers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adopter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Adopter__3214EC078A9CC691");

            entity.ToTable("Adopter", "roles");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AdoptionRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Adoption__3214EC07BC3E7D39");

            entity.ToTable("AdoptionRequest", "adoption");

            entity.HasIndex(e => e.AdopterId, "IX_AdoptionRequest_AdopterId");

            entity.HasIndex(e => e.PetId, "IX_AdoptionRequest_PetId");

            entity.HasIndex(e => e.Status, "IX_AdoptionRequest_Status");

            entity.Property(e => e.AdoptionDate).HasColumnType("datetime");
            entity.Property(e => e.RequestDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Adopter).WithMany(p => p.AdoptionRequests)
                .HasForeignKey(d => d.AdopterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdoptionRequest_Adopter");

            entity.HasOne(d => d.Pet).WithMany(p => p.AdoptionRequests)
                .HasForeignKey(d => d.PetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdoptionRequest_Pet");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pet__3214EC07AA9F8D8D");

            entity.ToTable("Pet", "adoption");

            entity.HasIndex(e => e.IsAdopted, "IX_Pet_IsAdopted");

            entity.HasIndex(e => e.VolunteerId, "IX_Pet_VolunteerId");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Species)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Volunteer).WithMany(p => p.Pets)
                .HasForeignKey(d => d.VolunteerId)
                .HasConstraintName("FK_Pet_Volunteer");
        });

        modelBuilder.Entity<Volunteer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Voluntee__3214EC077F356B48");

            entity.ToTable("Volunteer", "roles");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("User", "auth");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken", "auth");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsRequired();

            entity.Property(e => e.Expires)
                .HasColumnType("datetime");

            entity.Property(e => e.IsRevoked)
                .IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RefreshToken_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
