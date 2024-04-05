using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Booking;
using MyApiNetCore6.Models.Page;

namespace MyApiNetCore6.Repositories.ClientRepo
{
    public interface IPageRepository
    {
        public Task<Response> GetAllHotelsWithRoomTypesAsync();
        public Task<Response> GetHotelDetailAsync(int hotelId);
        public Task<Response> GetBedTypeByRoomTypeAsync(int roomTypeId);
        public Task<Response> BookingNowAsync(BookingNowModel bookingNowModel, string userId);
        public Task<Response> CheckRoomTypeAvailabilityAsync(CheckRoomTypeAvailabilityModel checkRoomTypeAvailabilityModel);
        public Task<Response> GetBookingByUserAsync(string userId);
        public Task<Response> GetBookingDetailsByIdAsync(int bookingOnlineId);
        public Task<Response> CancelBookingAsync(int bookingOnlineId);

    }
}
