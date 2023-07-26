namespace Casgem.ApiLayer.DTOs
{
    public class LoginInfoResponse
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
