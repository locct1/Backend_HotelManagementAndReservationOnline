using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Device;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IDeviceRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddDeviceModel model);
        public Task<Response> UpdateAsync(int id, UpdateDeviceModel model);
        public Task<Response> DeleteAsync(int id);
    }
}
