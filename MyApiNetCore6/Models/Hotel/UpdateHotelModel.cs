﻿namespace MyApiNetCore6.Models.Hotel
{
    public class UpdateHotelModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string FileName { get; set; }
        public string Info { get; set; }
    }
}
