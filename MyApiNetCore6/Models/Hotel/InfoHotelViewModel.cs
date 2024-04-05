using System.Text.Json.Serialization;

namespace MyApiNetCore6.Models.Hotel
{
    public class InfoHotelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Info { get; set; }
        public string FileName { get; set; }
        public bool Disabled { get; set; }
        [JsonIgnore]
        public List<HotelAccount> Users { get; set; }
        public HotelAccount HotelAccount { get; set; }
    }
    public class HotelAccount
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Disabled { get; set; }
        public string Role { get; set; }

    }
}
