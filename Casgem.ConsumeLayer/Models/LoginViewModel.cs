﻿namespace Casgem.ConsumeLayer.Models
{
    public class LoginViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
