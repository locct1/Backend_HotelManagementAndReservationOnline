using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyApiNetCore6.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<Product>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Products)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Room>()
             .HasOne(r => r.RoomType)
             .WithMany(r => r.Rooms)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomType>()
            .HasOne(h => h.Hotel)
            .WithMany(r => r.RoomTypes)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomType>()
            .HasOne(b => b.BedType)
            .WithMany(r => r.RoomTypes)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ApplicationUser>()
           .HasOne(b => b.Hotel)
           .WithMany(r => r.Users)
           .IsRequired(false)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HotelFacility>(entity =>
            {
                entity.HasKey(c => new { c.HotelId, c.FacilityId });
            });
            modelBuilder.Entity<DeviceRoomType>(entity =>
            {
                entity.HasKey(c => new { c.DeviceId, c.RoomTypeId });
            });
            modelBuilder.Entity<BookingOnlineDetail>(entity =>
            {
                entity.HasKey(c => new { c.BookingOnlineId, c.RoomTypeId });
            });

            modelBuilder.Entity<BookingOnlineDetail>()
                        .HasOne<BookingOnline>(sc => sc.BookingOnline)
                        .WithMany(s => s.BookingOnlineDetails)
                        .HasForeignKey(sc => sc.BookingOnlineId);



            modelBuilder.Entity<BookingOnlineDetail>()
                        .HasOne<RoomType>(sc => sc.RoomType)
                        .WithMany(s => s.BookingOnlineDetails)
                        .HasForeignKey(sc => sc.RoomTypeId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<BookingOnline>()
                        .HasOne<Hotel>(s => s.Hotel)
                        .WithMany(g => g.BookingOnlines)
                        .HasForeignKey(s => s.HotelId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RoomReservation>(entity =>
            {
                entity.HasKey(c => new { c.RoomId, c.ReservationId });
            });
            modelBuilder.Entity<RoomReservation>()
                        .HasKey(b => b.Id);

            modelBuilder.Entity<RoomReservationProduct>(entity =>
            {
                entity.HasKey(c => new { c.RoomRervationId, c.ProductId });
            });
            modelBuilder.Entity<RoomReservationProduct>()
                        .HasKey(b => b.Id);
            modelBuilder.Entity<RoomReservationProduct>()
                        .HasOne<Product>(s => s.Product)
                        .WithMany(g => g.RoomReservationProducts)
                        .HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.NoAction);
        }


        #region DbSet
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<RoomType>? RoomTypes { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<BedType>? BedTypes { get; set; }
        public DbSet<Facility>? Facilities { get; set; }
        public DbSet<FacilityType>? FacilityTypes { get; set; }
        public DbSet<HotelFacility>? HotelFacilities { get; set; }
        public DbSet<RoomTypePhoto>? RoomTypePhotos { get; set; }
        public DbSet<HotelPhoto>? HotelPhotos { get; set; }
        public DbSet<Device>? Devices { get; set; }
        public DbSet<DeviceRoomType>? DeviceRoomTypes { get; set; }
        public DbSet<BookingOnline>? BookingOnlines { get; set; }
        public DbSet<BookingOnlineDetail>? BookingOnlineDetails { get; set; }
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Status>? Statuses { get; set; }
        public DbSet<ClientOffline>? ClientOfflines { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<RoomReservation>? RoomReservations { get; set; }
        public DbSet<RoomReservationProduct>? RoomReservationProducts { get; set; }
        public DbSet<MethodPayment>? MethodPayments { get; set; }



        #endregion
    }
}