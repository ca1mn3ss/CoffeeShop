using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop1.Data;

public partial class CoffeeShopContext : DbContext
{
    public CoffeeShopContext()
    {
    }

    public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Baristum> Barista { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientDrink> ClientDrinks { get; set; }

    public virtual DbSet<Component> Components { get; set; }

    public virtual DbSet<ComponentDrink> ComponentDrinks { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<DrinkAvailability> DrinkAvailabilities { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<Storage> Storages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-FKBVM4E\\SQLEXPRESS; Database=CoffeeShop; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Baristum>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idshop).HasColumnName("IDShop");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.IdshopNavigation).WithMany(p => p.Barista)
                .HasForeignKey(d => d.Idshop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Barista_Shop1");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<ClientDrink>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idclient).HasColumnName("IDClient");
            entity.Property(e => e.Iddrink).HasColumnName("IDDrink");

            entity.HasOne(d => d.IdclientNavigation).WithMany(p => p.ClientDrinks)
                .HasForeignKey(d => d.Idclient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientDrinks_Client");

            entity.HasOne(d => d.IddrinkNavigation).WithMany(p => p.ClientDrinks)
                .HasForeignKey(d => d.Iddrink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientDrinks_Drink");
        });

        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Ingredient");

            entity.ToTable("Component");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idstorage).HasColumnName("IDStorage");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.IdstorageNavigation).WithMany(p => p.Components)
                .HasForeignKey(d => d.Idstorage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Component_Storage");
        });

        modelBuilder.Entity<ComponentDrink>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idcomponent).HasColumnName("IDComponent");
            entity.Property(e => e.Iddrink).HasColumnName("IDDrink");

            entity.HasOne(d => d.IdcomponentNavigation).WithMany(p => p.ComponentDrinks)
                .HasForeignKey(d => d.Idcomponent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComponentDrinks_Component");

            entity.HasOne(d => d.IddrinkNavigation).WithMany(p => p.ComponentDrinks)
                .HasForeignKey(d => d.Iddrink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComponentDrinks_Drink");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.ToTable("Drink");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IdcoffeeShop).HasColumnName("IDCoffeeShop");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NameOfShop).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<DrinkAvailability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DrinkAvailability_1");

            entity.ToTable("DrinkAvailability");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdcoffeeShop).HasColumnName("IDCoffeeShop");
            entity.Property(e => e.Iddrinks).HasColumnName("IDDrinks");

            entity.HasOne(d => d.IdcoffeeShopNavigation).WithMany(p => p.DrinkAvailabilities)
                .HasForeignKey(d => d.IdcoffeeShop)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DrinkAvailability_Shop");

            entity.HasOne(d => d.IddrinksNavigation).WithMany(p => p.DrinkAvailabilities)
                .HasForeignKey(d => d.Iddrinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DrinkAvailability_Drink1");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.ToTable("Shop");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<Storage>(entity =>
        {
            entity.ToTable("Storage");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Provisioner).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
