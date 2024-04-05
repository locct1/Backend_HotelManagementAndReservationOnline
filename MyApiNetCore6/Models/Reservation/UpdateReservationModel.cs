namespace MyApiNetCore6.Models.Reservation
{
    public class UpdateReservationModel
    {
        public UpdateInfoClientOffline InfoClientOffline { get; set; }
        public UpdateReservation UpdateReservation { get; set; }
    }
    public class UpdateInfoClientOffline
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
    public class UpdateReservation
    {
        public int Id { get; set; }
        public int? HotelId { get; set; }
        public int? BookingOnlineId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelPhoneNumber { get; set; }
        public double Total { get; set; }
        public int AmountOfPeople { get; set; }
        public int AmountOfNight { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StatusId { get; set; }
        public List<UpdateRoomReservation>? RoomReservations { get; set; }
    }
    public class UpdateRoomReservation
    {
        public int Id { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomName { get; set; }
        public string BedTypeName { get; set; }
        public double Price { get; set; }
        public int RoomId { set; get; }
        public int? ReservationId { set; get; }
        public List<UpdateRoomReservationProduct>? roomReservationProducts { get; set; }

    }
    public class UpdateRoomReservationProduct
    {
        public int Id { get; set; }
        public int? RoomRervationId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
    }
}
