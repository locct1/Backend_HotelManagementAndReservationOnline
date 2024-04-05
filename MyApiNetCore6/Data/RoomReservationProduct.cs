using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("RoomReservationProduct")]

    public class RoomReservationProduct
    {

        [Key]
        public int Id { get; set; }
        public int? RoomRervationId { get; set; }
        [ForeignKey("RoomRervationId")]
        public RoomReservation? RoomReservation { get; set; }
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
    }
}
