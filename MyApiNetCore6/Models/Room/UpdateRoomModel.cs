namespace MyApiNetCore6.Models.Room
{
    public class UpdateRoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int RoomTypeId { get; set; }
    }
}
