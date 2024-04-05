using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApiNetCore6.Data
{
    [Table("Facility")]
    public class Facility
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set; }
        public int? FacilityTypeId { get; set; }
        [ForeignKey("FacilityTypeId")]
        public FacilityType FacilityType { get; set; }
        public List<HotelFacility> HotelFacilities { get; set; }
    }
}
