using MyApiNetCore6.Models;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IAdminBookingOnlineRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> UpdateStatusAsync(int bookingOnlineId, int statusId);
        public Task<Response> GetAsync(int bookingOnlineId);
    }
}
