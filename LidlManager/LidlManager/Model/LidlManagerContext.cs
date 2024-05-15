using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LidlManager.Model;

public partial class LidlManagerContext : DbContext
{
    public LidlManagerContext()
    {
    }

    public LidlManagerContext(DbContextOptions<LidlManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductReceipt> ProductReceipts { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5P6VIV2\\SQLEXPRESS;User ID=user;Password=1234!Secret;Database=LidlManager;Trusted_Connection=false;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producer>(entity =>
        {
            entity.ToTable("Producer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CountryOfOrigin)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Barcode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdProducer).HasColumnName("ID_producer");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProducerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProducer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Producer");
        });

        modelBuilder.Entity<ProductReceipt>(entity =>
        {
            entity.ToTable("ProductReceipt");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");
            entity.Property(e => e.IdReceipt).HasColumnName("Id_receipt");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.ProductReceipts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Receipt");

            entity.HasOne(d => d.IdReceiptNavigation).WithMany(p => p.ProductReceipts)
                .HasForeignKey(d => d.IdReceipt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Receipt2");
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.ToTable("Receipt");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FloatTotalSum).HasColumnName("floatTotalSum");
            entity.Property(e => e.IdUser).HasColumnName("Id_user");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipt_User");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdProduct).HasColumnName("Id_product");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_Product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
