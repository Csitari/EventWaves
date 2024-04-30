using Microsoft.AspNetCore.Http;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Unicode;
using Vizsgaremek_Backend.Models;
using Vizsgaremek_Backend.Models.JWT;

namespace Vizsgaremek_Backend.Controllers.JWT
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpPost("regisztracio")]
        public ActionResult<User> Register(UserDto request)
        {
            Felhasznalok user = new Felhasznalok();
            user.FelhasznaloId = Guid.NewGuid();
            user.Felhasznalonev = request.Username;
            user.Email = request.Email;
            user.Telefonszam = request.Phone;
            user.SzerepId = 2;
            user.Salt = "salt";
            user.VarosId = 13;
            
            try
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.JelszoHash = passwordHash;

                using (var context = new EsemenytarContext())
                {
                    if (context != null)
                    {
                        context.Felhasznaloks.Add(user);
                        context.SaveChanges();
                        return Ok("Felhasználó sikeresen regisztrált!");
                    }
                    else
                    {
                        return BadRequest("Hiba a regisztráció során.");
                    }
                }

                    
            }
            catch (Exception ex)
            {
                return StatusCode(400,$"Hiba történt: {ex.Message}");
            }
        }

        [HttpPost("bejelentkezes")]
        public ActionResult<User> Login(LoginDto request)
        {
            using (var context = new EsemenytarContext()) {
                User userBejelentkezes = new User();
                var megtalalt = context.Felhasznaloks.FirstOrDefault(x => x.Felhasznalonev == request.Username);
                

                if (megtalalt == null)
                {
                    return BadRequest("Cigány vagy?");

                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, megtalalt.JelszoHash))
                {
                    return BadRequest("Hibás jelszó, felhasználó név");
                }
                userBejelentkezes.Id = megtalalt.FelhasznaloId;
                userBejelentkezes.Username = megtalalt.Felhasznalonev;
                userBejelentkezes.Email = megtalalt.Email;
                userBejelentkezes.Letrehozva = megtalalt.Letrehozva;
                userBejelentkezes.Szerep_Id = (int)megtalalt.SzerepId;
                userBejelentkezes.Telefonszam = megtalalt.Telefonszam;
                userBejelentkezes.Varos_Id = (int)megtalalt.VarosId;
                

                string token = CreateToken(userBejelentkezes);
                Token token1 = new Token();
                token1.tokenValue = token;
                return Ok(token1);
            }
        }

        private string CreateToken(User user)
        {
            using (var context = new EsemenytarContext())
            {
                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.DateOfBirth, user.Letrehozva.ToString()),
                new Claim(ClaimTypes.PostalCode, user.Varos_Id.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.Telefonszam.ToString()),
                new Claim(ClaimTypes.Role, context.Szerepeks.FirstOrDefault(x=>x.SzerepId == user.Szerep_Id).Szerepnev)

            };

                var kulcs = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

                var cr = new SigningCredentials(kulcs, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    audience: _configuration.GetValue<string>("Authentication:Schemes:Bearer:ValidAudiences:0"),
                    issuer: _configuration.GetSection("Authentication:Schemes:Bearer:ValidIssuer").Value,
                    signingCredentials: cr);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
        }
    }
}
