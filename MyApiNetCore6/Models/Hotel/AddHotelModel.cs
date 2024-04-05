namespace MyApiNetCore6.Models.Hotel
{
    public class AddHotelModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public bool Disabled { get; set; }
    }
}
