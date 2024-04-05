using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApiNetCore6.Data
{
    [Table("HotelPhoto")]

    public class HotelPhoto
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }
    }
}
