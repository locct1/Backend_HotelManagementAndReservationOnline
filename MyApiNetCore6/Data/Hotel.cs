using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace MyApiNetCore6.Data
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Info { get; set; }
        public bool Disabled { get; set; }
        public string? FileName { get; set; }

        public List<ApplicationUser> Users { get; set; }
        public List<Category> Categories { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<HotelFacility> HotelFacilities { get; set; }
        public List<HotelPhoto> HotelPhotos { get; set; }
        public List<BookingOnline> BookingOnlines { get; set; }
    }
}
