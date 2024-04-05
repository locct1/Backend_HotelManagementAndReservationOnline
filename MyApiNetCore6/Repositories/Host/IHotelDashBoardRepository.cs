using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IHotelDashBoardRepository
    {
        public Task<Response> GetAllAsync(int hotelId);
    }
}
