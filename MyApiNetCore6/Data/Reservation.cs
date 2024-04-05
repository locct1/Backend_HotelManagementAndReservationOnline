using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("Reservation")]

    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public int? HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel? Hotel { get; set; }
        [ForeignKey("BookingOnlineId")]
        public int? BookingOnlineId { get; set; }
        public BookingOnline? BookingOnline { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelPhoneNumber { get; set; }
        public double Total { get; set; }
        public int AmountOfPeople { get; set; }
        public int AmountOfNight { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<RoomReservation> RoomReservations { get; set; }
        public int ClientOfflineId { get; set; }
        [ForeignKey("ClientOfflineId")]
        public ClientOffline ClientOffline { get; set; }

        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
