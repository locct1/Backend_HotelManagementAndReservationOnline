namespace MyApiNetCore6.Models.Image
{
    public class HotelPhotoModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public int HotelId { get; set; }
    }
}
