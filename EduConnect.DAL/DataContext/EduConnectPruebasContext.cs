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

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chats__A9FBE62678CA6262");

            entity.Property(e => e.ChatId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ChatID");
            entity.Property(e => e.MatchId).HasColumnName("MatchID");
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Match).WithMany(p => p.Chats)
                .HasForeignKey(d => d.MatchId)
                .HasConstraintName("FK__Chats__MatchID__797309D9");

            entity.HasOne(d => d.Sender).WithMany(p => p.Chats)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Chats__SenderID__14270015");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21A967142F449");

            entity.Property(e => e.CityId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");

            entity.HasOne(d => d.Department).WithMany(p => p.Cities)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Cities__Departme__60A75C0F");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.CollegeId).HasName("PK__Colleges__2940951979CA4D35");

            entity.Property(e => e.CollegeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("CollegeID");
            entity.Property(e => e.AdditionalInfo).IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CityId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CityID");
            entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.City).WithMany(p => p.Colleges)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Colleges__CityID__6477ECF3");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCDA5A54A90");

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ADD2223EE8A");

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
                .HasConstraintName("FK__History__College__7E37BEF6");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__History__UserID__7D439ABD");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.MatchId).HasName("PK__Matches__4218C837340F52AB");

            entity.Property(e => e.MatchId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MatchID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RequestIdUser1).HasColumnName("RequestID_User1");
            entity.Property(e => e.RequestIdUser2).HasColumnName("RequestID_User2");

            entity.HasOne(d => d.RequestIdUser1Navigation).WithMany(p => p.MatchRequestIdUser1Navigations)
                .HasForeignKey(d => d.RequestIdUser1)
                .HasConstraintName("FK__Matches__Request__74AE54BC");

            entity.HasOne(d => d.RequestIdUser2Navigation).WithMany(p => p.MatchRequestIdUser2Navigations)
                .HasForeignKey(d => d.RequestIdUser2)
                .HasConstraintName("FK__Matches__Request__75A278F5");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519A8A0DC28C");

            entity.Property(e => e.RequestId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RequestID");
            entity.Property(e => e.CollegeId).HasColumnName("CollegeID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.College).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK__Requests__Colleg__70DDC3D8");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Requests__UserID__6FE99F9F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A4DEE7B05");

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD4B00E4D");

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
            entity.Property(e => e.Photo).IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.College).WithMany(p => p.Users)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("FK__Users__CollegeID__6C190EBB");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
