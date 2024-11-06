using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventFlowerExchange.Repositories.Entities;

public partial class EventFlowerExchangeContext : DbContext
{
    public EventFlowerExchangeContext()
    {
    }

    public EventFlowerExchangeContext(DbContextOptions<EventFlowerExchangeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Flower> Flowers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flower>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Flowers__3214EC07B015BCEC");

            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Seller).WithMany(p => p.Flowers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Flowers__SellerI__3A81B327");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07ED2A8969");

            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Buyer).WithMany(p => p.OrderBuyers)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("FK__Orders__BuyerId__3D5E1FD2");

            entity.HasOne(d => d.Flower).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FlowerId)
                .HasConstraintName("FK__Orders__FlowerId__3F466844");

            entity.HasOne(d => d.Seller).WithMany(p => p.OrderSellers)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__Orders__SellerId__3E52440B");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC074D557C6C");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Payments__OrderI__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07A879711C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534BD0815BA").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
