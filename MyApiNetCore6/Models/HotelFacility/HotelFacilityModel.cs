using MyApiNetCore6.Models.Facility;

namespace MyApiNetCore6.Models.HotelFacility
{
    public class HotelFacilityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public List<FacilityModel> Facilities { get; set; }
    }
}
