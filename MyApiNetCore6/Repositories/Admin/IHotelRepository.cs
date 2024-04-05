using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IHotelRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> GetAsync(int hotelId);
        public Task<Response> changeStatusHostAccountAsync(string userId);
        public Task<Response> changeStatusHotelAsync(int hotelId);
    }
}
