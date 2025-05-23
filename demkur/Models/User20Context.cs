using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace demkur.Models;

public partial class User20Context : DbContext
{
    public User20Context()
    {
    }

    public User20Context(DbContextOptions<User20Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=45.67.56.214;Port=5421;Database=user20;Username=user20;Password=uFTJmxd5");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pk");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Role1)
                .HasColumnType("character varying")
                .HasColumnName("Role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pk");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
