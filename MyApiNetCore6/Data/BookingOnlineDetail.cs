using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("BookingOnlineDetail")]
    public class BookingOnlineDetail
    {
        public string RoomTypeName { get; set; }
        public double Price { get; set; }
        public string BedTypeName { get; set; }
        public int AmountOfRoom { get; set; }
        public int BookingOnlineId { set; get; }

        public int? RoomTypeId { set; get; }

        [ForeignKey("BookingOnlineId")]
        public BookingOnline BookingOnline { set; get; }
        [ForeignKey("RoomTypeId")]
        public RoomType? RoomType { set; get; }
    }
}
