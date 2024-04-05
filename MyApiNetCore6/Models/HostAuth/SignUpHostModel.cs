using MyApiNetCore6.Models.Hotel;

namespace MyApiNetCore6.Models.HostAuth
{
    public class SignUpHostModel
    {
        public HostAccountModel User { get; set; }
        public AddHotelModel Hotel { get; set; }

    }
}
