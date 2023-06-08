using System;
using System.Collections.Generic;
using EduConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace EduConnect.DAL.DataContext;

public partial class EduConnectPruebasContext : DbContext
{
    public EduConnectPruebasContext()
    {
    }

    public EduConnectPruebasContext(DbContextOptions<EduConnectPruebasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=REVISION-PC; Initial Catalog=EduConnectPruebas; Integrated Security=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chats__A9FBE6260A370B06");

            entity.Property(e => e.ChatId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ChatID");
            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Sender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Match).WithMany(p => p.Chats)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK__Chats__MatchID__60A75C0F");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.CollegeId).HasName("PK__Colleges__294095194389AE09");

            entity.Property(e => e.CollegeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CollegeID");
            entity.Property(e => e.AdditionalInfo).IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ADD21C18F92");

            entity.ToTable("History");

            entity.Property(e => e.HistoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("HistoryID");
            entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            entity.Property(e => e.ChangeType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CollegeId).HasColumnName("CollegeID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.College).WithMany(p => p.Histories)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK__History__College__656C112C");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__History__UserID__6477ECF3");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Matches__4218C837B25220A5");

            entity.Property(e => e.MatchId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MatchID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RequestIdUser1).HasColumnName("RequestID_User1");
            entity.Property(e => e.RequestIdUser2).HasColumnName("RequestID_User2");

            entity.HasOne(d => d.RequestIdUser1Navigation).WithMany(p => p.MatchRequestIdUser1Navigations)
                .HasForeignKey(d => d.RequestIdUser1)
                .HasConstraintName("FK__Matches__Request__5BE2A6F2");

            entity.HasOne(d => d.RequestIdUser2Navigation).WithMany(p => p.MatchRequestIdUser2Navigations)
                .HasForeignKey(d => d.RequestIdUser2)
                .HasConstraintName("FK__Matches__Request__5CD6CB2B");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519ADC79D833");

            entity.Property(e => e.RequestId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RequestID");
            entity.Property(e => e.CollegeId).HasColumnName("CollegeID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.College).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK__Requests__Colleg__5812160E");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Requests__UserID__571DF1D5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3ABB08B47E");

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACF2D58CB5");

            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UserID");
            entity.Property(e => e.CollegeId).HasColumnName("CollegeID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Photo).HasColumnType("text");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.College).WithMany(p => p.Users)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK__Users__CollegeID__01142BA1");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
