namespace MyApiNetCore6.Models.RoomType
{
    public class UpdateRoomTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Max { get; set; }
        public int AmountOfSold { get; set; }

        public int BedTypeId { get; set; }
        public int HotelId { get; set; }
    }
}
