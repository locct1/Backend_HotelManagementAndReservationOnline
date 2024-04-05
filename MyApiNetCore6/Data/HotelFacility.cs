using MyApiNetCore6.Models.Service;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("HotelFacility")]
    public class HotelFacility
    {
        public int FacilityId { set; get; }

        public int HotelId { set; get; }

        [ForeignKey("FacilityId")]
        public Facility Facility { set; get; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { set; get; }
    }
}
