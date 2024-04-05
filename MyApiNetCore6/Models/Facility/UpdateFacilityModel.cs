namespace MyApiNetCore6.Models.Facility
{
    public class UpdateFacilityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int FacilityTypeId { get; set; }

    }
}
