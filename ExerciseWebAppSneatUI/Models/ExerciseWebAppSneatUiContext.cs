using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExerciseWebAppSneatUI.Models;

public partial class ExerciseWebAppSneatUiContext : DbContext
{
    public ExerciseWebAppSneatUiContext()
    {
    }

    public ExerciseWebAppSneatUiContext(DbContextOptions<ExerciseWebAppSneatUiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountUser> AccountUsers { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountUser>(entity =>
        {
            entity.ToTable("AccountUser");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(10);
            entity.Property(e => e.ClassSection).HasMaxLength(10);
            entity.Property(e => e.Tutor).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentNo);

            entity.ToTable("Student");

            entity.Property(e => e.Course).HasMaxLength(50);
            entity.Property(e => e.Grade).HasMaxLength(10);
            entity.Property(e => e.StudentName).HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.ToTable("Teacher");

            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Subject).HasMaxLength(50);
            entity.Property(e => e.TeacherName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
