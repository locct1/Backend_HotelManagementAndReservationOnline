namespace MyApiNetCore6.Models.HostAuth
{
    public class HostAccountModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool Disabled { get; set; }
        public int HotelId { get; set; }
    }
}
