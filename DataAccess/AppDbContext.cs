using Microsoft.EntityFrameworkCore;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<Hall> Halls => Set<Hall>();

    public DbSet<HallOption> HallOptions => Set<HallOption>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<BookingHallOption> BookingHallOptions => Set<BookingHallOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hall>(entity =>
        {
            entity.Property(r => r.HourlyRate)
                .HasPrecision(10, 2);

            entity.Property(r => r.IsActive)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<HallOption>(entity =>
        {
            entity.Property(s => s.Price)
                .HasPrecision(10, 2);

            entity.Property(s => s.IsActive)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(b => b.TotalPrice)
                .HasPrecision(12, 2);

            entity.Property(b => b.Status)
                .HasDefaultValue(BookingStatus.Confirmed);

            entity.Property(b => b.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(b => b.StartTime)
                .HasColumnType("timestamp without time zone");

            entity.Property(b => b.EndTime)
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<BookingHallOption>()
            .HasKey(bs => new { bs.BookingId, bs.HallOptionId });

        modelBuilder.Entity<Hall>().HasData(
            new Hall
            {
                Id = 1,
                Name = "Зал А",
                Capacity = 50,
                HourlyRate = 2000,
                IsActive = true
            },
            new Hall
            {
                Id = 2,
                Name = "Зал B",
                Capacity = 100,
                HourlyRate = 3500,
                IsActive = true
            },
            new Hall
            {
                Id = 3,
                Name = "Зал C",
                Capacity = 30,
                HourlyRate = 1500,
                IsActive = true
            }
);

        modelBuilder.Entity<HallOption>().HasData(
            new HallOption
            {
                Id = 1,
                Name = "Проектор",
                Price = 500,
                IsActive = true
            },
            new HallOption
            {
                Id = 2,
                Name = "Wi-Fi",
                Price = 300,
                IsActive = true
            },
            new HallOption
            {
                Id = 3,
                Name = "Звук",
                Price = 700,
                IsActive = true
            }
        );
    }
}