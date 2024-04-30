using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vizsgaremek_Backend.Models;

public partial class EsemenytarContext : DbContext
{
    public EsemenytarContext()
    {
    }

    public EsemenytarContext(DbContextOptions<EsemenytarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ErdekeltEsemenyKategoriak> ErdekeltEsemenyKategoriaks { get; set; }

    public virtual DbSet<EsemenyHozzaszolasok> EsemenyHozzaszolasoks { get; set; }

    public virtual DbSet<EsemenyInterakcio> EsemenyInterakcios { get; set; }

    public virtual DbSet<EsemenyKategoriak> EsemenyKategoriaks { get; set; }

    public virtual DbSet<Esemenyek> Esemenyeks { get; set; }

    public virtual DbSet<Felhasznalok> Felhasznaloks { get; set; }

    public virtual DbSet<Megyek> Megyeks { get; set; }

    public virtual DbSet<Szerepek> Szerepeks { get; set; }

    public virtual DbSet<Telepulesek> Telepuleseks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=esemenytar;user=root;password=;ssl mode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ErdekeltEsemenyKategoriak>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("erdekelt_esemeny_kategoriak");

            entity.HasIndex(e => e.FelhasznaloId, "felhasznalo_id");

            entity.HasIndex(e => e.KategoriaId, "kategoria_id");

            entity.Property(e => e.FelhasznaloId).HasColumnName("felhasznalo_id");
            entity.Property(e => e.KategoriaId).HasColumnName("kategoria_id");
            entity.Property(e => e.KategoriaPont).HasColumnName("kategoria_pont");

            entity.HasOne(d => d.Felhasznalo).WithMany()
                .HasForeignKey(d => d.FelhasznaloId)
                .HasConstraintName("erdekelt_esemeny_kategoriak_ibfk_3");

            entity.HasOne(d => d.Kategoria).WithMany()
                .HasForeignKey(d => d.KategoriaId)
                .HasConstraintName("erdekelt_esemeny_kategoriak_ibfk_2");
        });

        modelBuilder.Entity<EsemenyHozzaszolasok>(entity =>
        {
            entity.HasKey(e => e.HozzaszolasId).HasName("PRIMARY");

            entity.ToTable("esemeny_hozzaszolasok");

            entity.HasIndex(e => e.EsemenyId, "esemeny_id");

            entity.HasIndex(e => e.HozzaszoloId, "felhasznalo_id");

            entity.Property(e => e.HozzaszolasId).HasColumnName("hozzaszolas_id");
            entity.Property(e => e.EsemenyId).HasColumnName("esemeny_id");
            entity.Property(e => e.HozzaszolasSzoveg)
                .HasColumnType("text")
                .HasColumnName("hozzaszolas_szoveg");
            entity.Property(e => e.HozzaszoloId).HasColumnName("hozzaszolo_id");
            entity.Property(e => e.Letrehozva)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("letrehozva");

            entity.HasOne(d => d.Esemeny).WithMany(p => p.EsemenyHozzaszolasoks)
                .HasForeignKey(d => d.EsemenyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("esemeny_hozzaszolasok_ibfk_2");

            entity.HasOne(d => d.Hozzaszolo).WithMany(p => p.EsemenyHozzaszolasoks)
                .HasForeignKey(d => d.HozzaszoloId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("esemeny_hozzaszolasok_ibfk_1");
        });

        modelBuilder.Entity<EsemenyInterakcio>(entity =>
        {
            entity.HasKey(e => e.InterakcioId).HasName("PRIMARY");

            entity.ToTable("esemeny_interakcio");

            entity.HasIndex(e => e.EsemenyId, "esemeny_id");

            entity.HasIndex(e => new { e.FelhasznaloId, e.EsemenyId }, "felhasznalo_id").IsUnique();

            entity.Property(e => e.InterakcioId).HasColumnName("interakcio_id");
            entity.Property(e => e.EsemenyId).HasColumnName("esemeny_id");
            entity.Property(e => e.FelhasznaloId).HasColumnName("felhasznalo_id");
            entity.Property(e => e.JelentkezesDatum)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("jelentkezes_datum");
            entity.Property(e => e.JelentkezettE).HasColumnName("jelentkezett-e");
            entity.Property(e => e.KedveltE).HasColumnName("kedvelt-e");
            entity.Property(e => e.MentettE).HasColumnName("mentett-e");

            entity.HasOne(d => d.Esemeny).WithMany(p => p.EsemenyInterakcios)
                .HasForeignKey(d => d.EsemenyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("esemeny_interakcio_ibfk_2");

            entity.HasOne(d => d.Felhasznalo).WithMany(p => p.EsemenyInterakcios)
                .HasForeignKey(d => d.FelhasznaloId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("esemeny_interakcio_ibfk_1");
        });

        modelBuilder.Entity<EsemenyKategoriak>(entity =>
        {
            entity.HasKey(e => e.KategoriaId).HasName("PRIMARY");

            entity.ToTable("esemeny_kategoriak");

            entity.HasIndex(e => e.KategoriaNev, "kategoria_nev").IsUnique();

            entity.Property(e => e.KategoriaId).HasColumnName("kategoria_id");
            entity.Property(e => e.KategoriaNev)
                .HasMaxLength(64)
                .HasColumnName("kategoria_nev");
        });

        modelBuilder.Entity<Esemenyek>(entity =>
        {
            entity.HasKey(e => e.EsemenyId).HasName("PRIMARY");

            entity.ToTable("esemenyek");

            entity.HasIndex(e => e.Cim, "cim");

            entity.HasIndex(e => e.DislikeSzamlalo, "dislike_szamlalo");

            entity.HasIndex(e => e.KategoriaId, "kategoria");

            entity.HasIndex(e => e.KategoriaId, "kategoria_id");

            entity.HasIndex(e => e.Letrehozva, "letrehozva");

            entity.HasIndex(e => e.LikeSzamlalo, "like_szamlalo");

            entity.HasIndex(e => e.SzervezoId, "szervezo_id");

            entity.HasIndex(e => e.VarosId, "varos_id");

            entity.Property(e => e.EsemenyId).HasColumnName("esemeny_id");
            entity.Property(e => e.BoritoKep)
                .HasColumnType("text")
                .HasColumnName("borito_kep");
            entity.Property(e => e.Cim).HasColumnName("cim");
            entity.Property(e => e.DislikeSzamlalo).HasColumnName("dislike_szamlalo");
            entity.Property(e => e.Idopont)
                .HasColumnType("datetime")
                .HasColumnName("idopont");
            entity.Property(e => e.KategoriaId).HasColumnName("kategoria_id");
            entity.Property(e => e.Korhatar).HasColumnName("korhatar");
            entity.Property(e => e.Leiras)
                .HasColumnType("text")
                .HasColumnName("leiras");
            entity.Property(e => e.Letrehozva)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("letrehozva");
            entity.Property(e => e.LikeSzamlalo).HasColumnName("like_szamlalo");
            entity.Property(e => e.Statusz)
                .HasDefaultValueSql("'aktív'")
                .HasColumnType("enum('aktív','inaktív','lezárt')")
                .HasColumnName("statusz");
            entity.Property(e => e.SzervezoId).HasColumnName("szervezo_id");
            entity.Property(e => e.VarosId).HasColumnName("varos_id");

            entity.HasOne(d => d.Kategoria).WithMany(p => p.Esemenyeks)
                .HasForeignKey(d => d.KategoriaId)
                .HasConstraintName("esemenyek_ibfk_3");

            entity.HasOne(d => d.Szervezo).WithMany(p => p.Esemenyeks)
                .HasForeignKey(d => d.SzervezoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("esemenyek_ibfk_4");

            entity.HasOne(d => d.Varos).WithMany(p => p.Esemenyeks)
                .HasForeignKey(d => d.VarosId)
                .HasConstraintName("esemenyek_ibfk_2");
        });

        modelBuilder.Entity<Felhasznalok>(entity =>
        {
            entity.HasKey(e => e.FelhasznaloId).HasName("PRIMARY");

            entity.ToTable("felhasznalok");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Felhasznalonev, "felhasznalonev").IsUnique();

            entity.HasIndex(e => e.Letrehozva, "letrehozva");

            entity.HasIndex(e => e.LikeSzamlalo, "like_szamlalo");

            entity.HasIndex(e => e.SzerepId, "szerep_id");

            entity.HasIndex(e => e.VarosId, "varos_id");

            entity.Property(e => e.FelhasznaloId).HasColumnName("felhasznalo_id");
            entity.Property(e => e.Avatar)
                .HasColumnType("text")
                .HasColumnName("avatar");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Felhasznalonev)
                .HasMaxLength(64)
                .HasColumnName("felhasznalonev");
            entity.Property(e => e.JelszoHash)
                .HasMaxLength(64)
                .IsFixedLength()
                .HasColumnName("jelszo_hash");
            entity.Property(e => e.Leiras)
                .HasColumnType("text")
                .HasColumnName("leiras");
            entity.Property(e => e.Letrehozva)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("letrehozva");
            entity.Property(e => e.LikeSzamlalo).HasColumnName("like_szamlalo");
            entity.Property(e => e.Salt)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("salt");
            entity.Property(e => e.SzerepId).HasColumnName("szerep_id");
            entity.Property(e => e.Telefonszam)
                .HasMaxLength(20)
                .HasColumnName("telefonszam");
            entity.Property(e => e.VarosId).HasColumnName("varos_id");

            entity.HasOne(d => d.Szerep).WithMany(p => p.Felhasznaloks)
                .HasForeignKey(d => d.SzerepId)
                .HasConstraintName("felhasznalok_ibfk_1");

            entity.HasOne(d => d.Varos).WithMany(p => p.Felhasznaloks)
                .HasForeignKey(d => d.VarosId)
                .HasConstraintName("felhasznalok_ibfk_2");
        });

        modelBuilder.Entity<Megyek>(entity =>
        {
            entity.HasKey(e => e.MegyeId).HasName("PRIMARY");

            entity.ToTable("megyek");

            entity.HasIndex(e => e.Megyenev, "megyenev").IsUnique();

            entity.Property(e => e.MegyeId).HasColumnName("megye_id");
            entity.Property(e => e.Megyenev)
                .HasMaxLength(50)
                .HasColumnName("megyenev");
        });

        modelBuilder.Entity<Szerepek>(entity =>
        {
            entity.HasKey(e => e.SzerepId).HasName("PRIMARY");

            entity.ToTable("szerepek");

            entity.HasIndex(e => e.Szerepnev, "szerepnev").IsUnique();

            entity.Property(e => e.SzerepId).HasColumnName("szerep_id");
            entity.Property(e => e.Szerepnev)
                .HasMaxLength(24)
                .HasColumnName("szerepnev");
        });

        modelBuilder.Entity<Telepulesek>(entity =>
        {
            entity.HasKey(e => e.TelepulesId).HasName("PRIMARY");

            entity.ToTable("telepulesek");

            entity.HasIndex(e => e.Iranyitoszam, "iranyitoszam");

            entity.HasIndex(e => e.MegyeId, "megye_id");

            entity.Property(e => e.TelepulesId).HasColumnName("telepules_id");
            entity.Property(e => e.Iranyitoszam).HasColumnName("iranyitoszam");
            entity.Property(e => e.MegyeId).HasColumnName("megye_id");
            entity.Property(e => e.TelepulesNev)
                .HasMaxLength(50)
                .HasColumnName("telepules_nev");

            entity.HasOne(d => d.Megye).WithMany(p => p.Telepuleseks)
                .HasForeignKey(d => d.MegyeId)
                .HasConstraintName("telepulesek_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
