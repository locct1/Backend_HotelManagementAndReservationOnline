using MyApiNetCore6.Models.BedType;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Facility;

namespace MyApiNetCore6.Repositories.Admin
{
    public interface IFacilityRepository
    {
        public Task<Response> GetAllAsync();
        public Task<Response> GetAsync(int id);
        public Task<Response> AddAsync(AddFacilityModel model);
        public Task<Response> UpdateAsync(int id, UpdateFacilityModel model);
        public Task<Response> DeleteAsync(int id);
    }
}
