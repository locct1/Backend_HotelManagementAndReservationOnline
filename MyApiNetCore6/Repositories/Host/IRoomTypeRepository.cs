using MyApiNetCore6.Models.Service;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.RoomType;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IRoomTypeRepository
    {
        public Task<Response> GetAllAsync(int hotelId);
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddRoomTypeModel model);
        public Task<Response> UpdateAsync(int id, UpdateRoomTypeModel model);
        public Task<Response> DeleteAsync(int id);
    }
}
