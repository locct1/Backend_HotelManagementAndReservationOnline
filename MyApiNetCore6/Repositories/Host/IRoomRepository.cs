using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Room;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IRoomRepository
    {
        public Task<Response> GetAllAsync(int hotelId);
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddRoomModel model);
        public Task<Response> UpdateAsync(int id, UpdateRoomModel model);
        public Task<Response> DeleteAsync(int id);
        public Task<Response> ChangeStatusRoomAsync(int id);
    }
}
