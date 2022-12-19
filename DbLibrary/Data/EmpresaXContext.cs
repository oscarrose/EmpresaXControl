using System;
using System.Collections.Generic;
using DbLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DbLibrary.Data;

public partial class EmpresaXContext : DbContext
{
    public EmpresaXContext()
    {
    }

    public EmpresaXContext(DbContextOptions<EmpresaXContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientAddress> ClientAddresses { get; set; }

 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PkCliente_Id");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("client_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Identification)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identification");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.StatusClient)
                .HasDefaultValueSql("((1))")
                .HasColumnName("status_client");
        });

        modelBuilder.Entity<ClientAddress>(entity =>
        {
            entity.HasKey(e => e.AddressesId).HasName("PkAddresses_clients");

            entity.ToTable("client_addresses");

            entity.Property(e => e.AddressesId).HasColumnName("addresses_id");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.HouseNumber).HasColumnName("House_number");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("postal_code");
            entity.Property(e => e.StateName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("state_name");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientAddresses)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKClient_id_client_addresses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
