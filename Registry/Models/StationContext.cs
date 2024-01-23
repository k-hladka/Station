using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Registry.Models;

public partial class StationContext : DbContext
{
    public StationContext()
    {
    }

    public StationContext(DbContextOptions<StationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<JourneyInfo> JourneyInfos { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KATERYNA;Initial Catalog=Station;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cities__3213E83FAD86A241");

            entity.HasIndex(e => e.Name, "UQ__Cities__72E12F1BC33BB5AD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<JourneyInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Journey___3213E83F3C1944B2");

            entity.ToTable("Journey_info");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FromCityId).HasColumnName("from_city_id");
            entity.Property(e => e.JourneyTime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("journey_time");
            entity.Property(e => e.PassingCities).HasColumnName("passing_cities");
            entity.Property(e => e.Price)
                .HasColumnType("smallmoney")
                .HasColumnName("price");
            entity.Property(e => e.ToCityId).HasColumnName("to_city_id");

            entity.HasOne(d => d.FromCity).WithMany(p => p.JourneyInfoFromCities)
                .HasForeignKey(d => d.FromCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__from_c__403A8C7D");

            entity.HasOne(d => d.ToCity).WithMany(p => p.JourneyInfoToCities)
                .HasForeignKey(d => d.ToCityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__to_cit__4316F928");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Schedule__3213E83F5DEDE360");

            entity.ToTable("Schedule");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArriveDate)
                .HasColumnType("date")
                .HasColumnName("arrive_date");
            entity.Property(e => e.ArriveTime).HasColumnName("arrive_time");
            entity.Property(e => e.CountSeats).HasColumnName("count_seats");
            entity.Property(e => e.DepartureDate)
                .HasColumnType("date")
                .HasColumnName("departure_date");
            entity.Property(e => e.DepartureTime).HasColumnName("departure_time");
            entity.Property(e => e.TransportInfo).HasColumnName("transport_info");
            entity.Property(e => e.TypeTransportId).HasColumnName("type_transport_id");

            entity.HasOne(d => d.TransportInfoNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TransportInfo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__transp__49C3F6B7");

            entity.HasOne(d => d.TypeTransport).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TypeTransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Schedule__type_t__02FC7413");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transpor__3213E83FD0C78C74");

            entity.ToTable("Transport");

            entity.HasIndex(e => e.Name, "UQ__Transpor__72E12F1B62CB6FE9").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
