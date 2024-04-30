namespace Vizsgaremek_Backend.Models.JWT
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string ? Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime Letrehozva {  get; set; }

        public string ? Avatar {  get; set; }

        public string ? Telefonszam { get; set;}

        public string ? Varosnev { get; set; }

        public int Varos_Id { get; set; }

        public int Szerep_Id { get; set; }
    }
}
