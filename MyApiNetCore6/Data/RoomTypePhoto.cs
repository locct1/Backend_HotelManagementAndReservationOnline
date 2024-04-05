using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("RoomTypePhoto")]
    public class RoomTypePhoto
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }
    }
}
