namespace Vizsgaremek_Backend.Models.JWT
{
    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
