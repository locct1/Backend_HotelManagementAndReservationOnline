using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("RoomReservation")]

    public class RoomReservation
    {
        [Key]
        public int Id { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomName { get; set; }
        public string BedTypeName { get; set; }
        public double Price { get; set; }
        public int RoomId { set; get; }

        public int? ReservationId { set; get; }

        [ForeignKey("ReservationId")]
        public Reservation? Reservation { set; get; }
        [ForeignKey("RoomId")]
        public Room? Room { set; get; }
        public List<RoomReservationProduct> RoomReservationProducts { get; set; }
    }
}
