using MyApiNetCore6.Models;
using MyApiNetCore6.Models.HostAuth;
using MyApiNetCore6.Models.Hotel;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IHostAccountRepository
    {
        public Task<Response> SignUpAsync(SignUpHostModel model);
        public Task<ResponseToken> SignInAsync(SignInHostModel model);
        public Task<Response> GetInfoHostAsync(string Id);
        public Task<Response> UpdateHotelAsync(int id, UpdateHotelModel updateHotelModel);
    }
}
