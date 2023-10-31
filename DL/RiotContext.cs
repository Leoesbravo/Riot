using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class RiotContext : DbContext
{
    public RiotContext()
    {
    }

    public RiotContext(DbContextOptions<RiotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetallesPartidum> DetallesPartida { get; set; }

    public virtual DbSet<Enfrentamiento> Enfrentamientos { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<Partidum> Partida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= Riot; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetallesPartidum>(entity =>
        {
            entity.HasKey(e => e.IdDetallesPartida).HasName("PK__Detalles__9989AE5CAD392492");

            entity.HasOne(d => d.IdParticipanteDosNavigation).WithMany(p => p.DetallesPartidumIdParticipanteDosNavigations)
                .HasForeignKey(d => d.IdParticipanteDos)
                .HasConstraintName("FK__DetallesP__IdPar__2D27B809");

            entity.HasOne(d => d.IdParticipanteUnoNavigation).WithMany(p => p.DetallesPartidumIdParticipanteUnoNavigations)
                .HasForeignKey(d => d.IdParticipanteUno)
                .HasConstraintName("FK__DetallesP__IdPar__2C3393D0");

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.DetallesPartida)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK__DetallesP__IdPar__2B3F6F97");
        });

        modelBuilder.Entity<Enfrentamiento>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Enfrentamiento");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.IdParticipante).HasName("PK__Particip__561392421FDEFCD5");

            entity.ToTable("Participante");

            entity.Property(e => e.Campeon)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Side)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPartidaNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdPartida)
                .HasConstraintName("FK__Participa__IdPar__286302EC");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Participa__IdUsu__276EDEB3");
        });

        modelBuilder.Entity<Partidum>(entity =>
        {
            entity.HasKey(e => e.IdPartida).HasName("PK__Partida__6ED660C7962AEC8E");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9757202929");

            entity.ToTable("Usuario");

            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
