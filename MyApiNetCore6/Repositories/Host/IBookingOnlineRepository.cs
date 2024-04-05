using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IBookingOnlineRepository
    {
        public Task<Response> GetAllAsync(int hotelId);
        public Task<Response> UpdateStatusAsync(int bookingOnlineId, int statusId);
        public Task<Response> GetAsync(int bookingOnlineId);
    }
}
