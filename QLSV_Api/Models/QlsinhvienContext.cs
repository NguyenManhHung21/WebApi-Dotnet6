using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLSV_Api.Models;

public partial class QlsinhvienContext : DbContext
{
    public QlsinhvienContext()
    {
    }

    public QlsinhvienContext(DbContextOptions<QlsinhvienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5I21JQC\\SQLEXPRESS;Database=QLSINHVIEN;Trusted_Connection=True;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.IdFaculty).HasName("PK__Faculty__0D72F2BFC6DE093B");

            entity.ToTable("Faculty");

            entity.Property(e => e.IdFaculty).ValueGeneratedNever();
            entity.Property(e => e.IndexFaculty)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.NameFaculty)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.IdStd).HasName("PK__Student__2B03B8E5684FEA0C");

            entity.ToTable("Student");

            entity.Property(e => e.Birthday).HasColumnType("date");

            entity.Property(e => e.Gender)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.NameStd).HasMaxLength(20);

            entity.HasOne(d => d.IdFacultyNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.IdFaculty)
                .HasConstraintName("FK_FacultyStudent");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
