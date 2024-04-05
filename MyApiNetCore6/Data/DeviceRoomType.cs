using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("DeviceRoomType")]

    public class DeviceRoomType
    {
        public int RoomTypeId { set; get; }

        public int DeviceId { set; get; }

        [ForeignKey("DeviceId")]
        public Device Device { set; get; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { set; get; }
    }
}
