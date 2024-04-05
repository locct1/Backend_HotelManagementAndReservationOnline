using MyApiNetCore6.Models;
using MyApiNetCore6.Models.BedType;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IBedTypeRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddBedTypeModel model);
        public Task<Response> UpdateAsync(int id, UpdateBedTypeModel model);
        public Task<Response> DeleteAsync(int id);
    }
}
