using MyApiNetCore6.Models;
using MyApiNetCore6.Models.DeviceRoomType;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IDeviceRoomTypeRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> GetAllDeviceRoomTypeAsync(int roomTypeId);
        public Task<Response> AddAsync(AddDeviceRoomTypesModel model);
    }
}
