using MyApiNetCore6.Models.BedType;
using MyApiNetCore6.Models.Device;

namespace MyApiNetCore6.Models.RoomType
{
    public class RoomTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Max { get; set; }
        public int AmountOfSold { get; set; }
        public List<DeviceRoomTypeModel> DeviceRoomTypes { get; set; }

        public BedTypeModel BedType { get; set; }

    }
    public class DeviceRoomTypeModel
    {
        public DeviceModel Device { get; set; }
    }

}