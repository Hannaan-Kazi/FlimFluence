using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataService;

public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<ImageTable> ImageTables { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WatchLater> WatchLaters { get; set; }

    public virtual DbSet<Watched> Watcheds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LP013\\SQLEXPRESS;Initial Catalog=ProjectDb;User ID=api;Password=root123; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE488BE31999C");

            entity.HasIndex(e => e.Email, "uc_unique_ad").IsUnique();

            entity.Property(e => e.CreatedBy).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HashedPassword).HasColumnType("text");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Salt).HasColumnType("text");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ImageTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageTab__3214EC27AB42C4BD");

            entity.ToTable("ImageTable");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941AA4958E49");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PosterUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Rating).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.Ratings).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.Summary).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Ratings__FCCDF87CFA48A2D8");

            entity.ToTable(tb => tb.HasTrigger("trg_UpdateAverageRating"));

            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Rating1)
                .HasColumnType("decimal(5, 1)")
                .HasColumnName("Rating");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Movie).WithMany(p => p.RatingsNavigation)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Ratings__MovieId__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Ratings__UserId__5165187F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C08CBB1A5");

            entity.HasIndex(e => e.Email, "uc_unique").IsUnique();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HashedPassword).HasColumnType("text");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PictureUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Salt).HasColumnType("text");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<WatchLater>(entity =>
        {
            entity.HasKey(e => e.WatchLaterId).HasName("PK__WatchLat__E507458E37453F35");

            entity.ToTable("WatchLater");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.WatchLaters)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__WatchLate__UserI__4AB81AF0");
        });

        modelBuilder.Entity<Watched>(entity =>
        {
            entity.HasKey(e => e.WatchedId).HasName("PK__Watched__C5F08D45ACA8AEC8");

            entity.ToTable("Watched");

            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Rating).HasColumnType("decimal(5, 1)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Watcheds)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Watched__UserId__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
