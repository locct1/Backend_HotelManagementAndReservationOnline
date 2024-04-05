using MyApiNetCore6.Models.RoomType;

namespace MyApiNetCore6.Models.Room
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public RoomTypeModel RoomType { get; set; }

    }
}
