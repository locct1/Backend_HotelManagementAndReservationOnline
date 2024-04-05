using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("RoomType")]
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Max { get; set; }
        public int AmountOfSold { get; set; }
        public List<Room> Rooms { get; set; }
        public int? HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
        public int? BedTypeId { get; set; }
        [ForeignKey("BedTypeId")]
        public BedType BedType { get; set; }
        public List<RoomTypePhoto> RoomTypePhotos { get; set; }
        public List<DeviceRoomType> DeviceRoomTypes { get; set; }
        public List<BookingOnlineDetail> BookingOnlineDetails { get; set; }
    }
}
