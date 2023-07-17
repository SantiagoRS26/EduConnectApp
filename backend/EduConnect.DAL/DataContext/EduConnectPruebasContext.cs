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

    public virtual DbSet<ChatMessage> ChatMessages { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chats__A9FBE6262A606313");

            entity.Property(e => e.ChatId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ChatID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.RequestId1).HasColumnName("RequestID1");
            entity.Property(e => e.RequestId2).HasColumnName("RequestID2");

            entity.HasOne(d => d.RequestId1Navigation).WithMany(p => p.ChatRequestId1Navigations)
                .HasForeignKey(d => d.RequestId1)
                .HasConstraintName("FK__Chats__RequestID__5070F446");

            entity.HasOne(d => d.RequestId2Navigation).WithMany(p => p.ChatRequestId2Navigations)
                .HasForeignKey(d => d.RequestId2)
                .HasConstraintName("FK__Chats__RequestID__5165187F");
        });

        modelBuilder.Entity<ChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__ChatMess__C87C037C002218A5");

            entity.Property(e => e.MessageId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("MessageID");
            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.SentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.ChatId)
                .HasConstraintName("FK__ChatMessa__ChatI__5535A963");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatMessages)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__ChatMessa__Sende__5629CD9C");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21A96CD8BECAC");

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
                .HasConstraintName("FK__Cities__Departme__398D8EEE");
        });

        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.CollegeId).HasName("PK__Colleges__29409519A5448B92");

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
                .HasConstraintName("FK__Colleges__CityID__3D5E1FD2");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD68C28FA3");

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
            entity.HasKey(e => e.HistoryId).HasName("PK__History__4D7B4ADDBAC3D774");

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
                .HasConstraintName("FK__History__College__5AEE82B9");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__History__UserID__59FA5E80");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8519ADA77A19D");

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
                .HasConstraintName("FK__Requests__Colleg__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Requests__UserID__4BAC3F29");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A51C5CF65");

            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACC8CA1215");

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
                .HasConstraintName("FK__Users__CollegeID__44FF419A");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
