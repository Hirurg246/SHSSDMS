using System;
using System.Collections.Generic;
using DataStorageModule.Models;
using Microsoft.EntityFrameworkCore;

namespace DataStorageModule.Data;

public partial class ShssdmsdbContext : DbContext
{
    public ShssdmsdbContext()
    {
    }

    public ShssdmsdbContext(DbContextOptions<ShssdmsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Camera> Cameras { get; set; }

    public virtual DbSet<Lock> Locks { get; set; }

    public virtual DbSet<LockUnlock> LockUnlocks { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Terminal> Terminals { get; set; }

    public virtual DbSet<TerminalAccess> TerminalAccesses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VideoRecording> VideoRecordings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=SHSSDMSDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Camera>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Camera)
                .HasForeignKey<Camera>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Camera_Room");
        });

        modelBuilder.Entity<Lock>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RoomFromId).HasColumnName("RoomFrom_id");
            entity.Property(e => e.RoomToId).HasColumnName("RoomTo_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.RoomFrom).WithMany(p => p.LockRoomFroms)
                .HasForeignKey(d => d.RoomFromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LockRF_Room");

            entity.HasOne(d => d.RoomTo).WithMany(p => p.LockRoomTos)
                .HasForeignKey(d => d.RoomToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LockRT_Room");

            entity.HasMany(d => d.Users).WithMany(p => p.Locks)
                .UsingEntity<Dictionary<string, object>>(
                    "LockAccess",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LockAccesse_User"),
                    l => l.HasOne<Lock>().WithMany()
                        .HasForeignKey("LockId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_LockAccesse_Lock"),
                    j =>
                    {
                        j.HasKey("LockId", "UserId");
                        j.ToTable("LockAccesses");
                        j.IndexerProperty<int>("LockId").HasColumnName("lock_id");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<LockUnlock>(entity =>
        {
            entity.HasKey(e => new { e.LockId, e.UnlockUserId, e.UnlockTime });

            entity.Property(e => e.LockId).HasColumnName("lock_id");
            entity.Property(e => e.UnlockUserId).HasColumnName("unlockUser_id");
            entity.Property(e => e.UnlockTime)
                .HasColumnType("datetime")
                .HasColumnName("unlockTime");
            entity.Property(e => e.RelockTime)
                .HasColumnType("datetime")
                .HasColumnName("relockTime");

            entity.HasOne(d => d.Lock).WithMany(p => p.LockUnlocks)
                .HasForeignKey(d => d.LockId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LockUnlock_Lock");

            entity.HasOne(d => d.UnlockUser).WithMany(p => p.LockUnlocks)
                .HasForeignKey(d => d.UnlockUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LockUnlock_UnlockUser");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(30)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Floor).HasColumnName("floor");
            entity.Property(e => e.Number).HasColumnName("number");
        });

        modelBuilder.Entity<Terminal>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.RoomId).HasColumnName("room_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Room).WithMany(p => p.Terminals)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Terminal_Room");
        });

        modelBuilder.Entity<TerminalAccess>(entity =>
        {
            entity.HasKey(e => new { e.TerminalId, e.UserId });

            entity.Property(e => e.TerminalId).HasColumnName("terminal_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");

            entity.HasOne(d => d.Terminal).WithMany(p => p.TerminalAccesses)
                .HasForeignKey(d => d.TerminalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TerminalAccesse_Terminal");

            entity.HasOne(d => d.User).WithMany(p => p.TerminalAccesses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TerminalAccesse_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.PasswodHash)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("passwodHash");
            entity.Property(e => e.Role).HasColumnName("role");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Person");
        });

        modelBuilder.Entity<VideoRecording>(entity =>
        {
            entity.HasKey(e => new { e.CameraId, e.TimeStart });

            entity.Property(e => e.CameraId).HasColumnName("camera_id");
            entity.Property(e => e.TimeStart)
                .HasColumnType("datetime")
                .HasColumnName("timeStart");
            entity.Property(e => e.TimeEnd)
                .HasColumnType("datetime")
                .HasColumnName("timeEnd");

            entity.HasOne(d => d.Camera).WithMany(p => p.VideoRecordings)
                .HasForeignKey(d => d.CameraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoRecording_Camera");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
