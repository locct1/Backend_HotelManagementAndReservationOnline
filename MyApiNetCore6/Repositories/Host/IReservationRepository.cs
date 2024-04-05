using MyApiNetCore6.Models;
using MyApiNetCore6.Models.Reservation;

namespace MyApiNetCore6.Repositories.Host
{
    public interface IReservationRepository
    {
        public Task<Response> AddReservationAsync(AddReservationModel model);
        public Task<Response> UpdateReservationAsync(UpdateReservationModel model);
        public Task<Response> UpdateReservationStatusAsync(int reservationId, int statusId);
        public Task<Response> CheckRoomAvailabilityAsync(CheckRoomAvailabilityModel checkRoomAvailabilityModel, int hotelId);

        public Task<Response> GetAllAsync(int hotelId);

    }
}
