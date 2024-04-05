using MyApiNetCore6.Models.Facility;
using MyApiNetCore6.Models;
using MyApiNetCore6.Models.HotelFacility;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IHotelFacilityRepository
    {
        public Task<Response> GetAllTypesAndFacilitiesAsync();
        public Task<Response> GetHotelFaticitiesAsync(int hotelId);
        public Task<Response> AddFacilitiesForHotelAsync(HotelAddFacilitiesModel model);
    }
}
