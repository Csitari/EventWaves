namespace Vizsgaremek_Backend.Models.JWT
{
    public class UserDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public required string Phone { get; set; }

       // public required string Varosnev { get; set;}
    }
}
