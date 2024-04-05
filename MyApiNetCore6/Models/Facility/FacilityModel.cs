using MyApiNetCore6.Models.CategoryService;
using MyApiNetCore6.Models.TypeFacility;

namespace MyApiNetCore6.Models.Facility
{
    public class FacilityModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Icon { get; set; }
        public FacilityTypeModel FacilityType { get; set; }


    }
}
