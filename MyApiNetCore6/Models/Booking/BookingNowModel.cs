namespace MyApiNetCore6.Models.Booking
{
    public class BookingNowModel
    {
        public InfoClient InfoClient { get; set; }
        public BookingOnlineModel BookingOnline { get; set; }
    }
    public class InfoClient
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
    public class BookingOnlineModel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelPhoneNumber { get; set; }
        public double Total { get; set; }
        public int AmountOfPeople { get; set; }
        public int AmountOfNight { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public int MethodPaymentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public RoomTypeBookingOnlineBModel RoomTypeBookingOnline { get; set; }

        public string? Onl_Amount { get; set; }
        public string? Onl_BankCode { get; set; }
        public string? Onl_OrderInfo { get; set; }
        public string? Onl_PayDate { get; set; }
        public string? Onl_TransactionStatus { get; set; }
        public string? Onl_SecureHash { get; set; }
        public string? Onl_TransactionNo { get; set; }
        public string? Onl_OrderId { get; set; }

    }
    public class RoomTypeBookingOnlineBModel
    {
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public double Price { get; set; }
        public string BedTypeName { get; set; }
        public int AmountOfRoom { get; set; }
    }
}