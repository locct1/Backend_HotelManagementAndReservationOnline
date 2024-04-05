﻿namespace MyApiNetCore6.Models.ClientAuth
{
    public class ClientViewModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Disabled { get; set; }
    }
}
