using MyApiNetCore6.Models.Hotel;

namespace MyApiNetCore6.Models.HostAuth
{
    public class HostViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Disabled { get; set; }
        public HotelViewModel? Hotel { get; set; }
    }
}
