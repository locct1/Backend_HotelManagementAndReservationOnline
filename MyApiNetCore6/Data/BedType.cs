using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("BedType")]

    public class BedType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoomType> RoomTypes { get; set; }
    }
}
