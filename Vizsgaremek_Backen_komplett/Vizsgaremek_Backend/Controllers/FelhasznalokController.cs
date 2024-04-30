using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FelhasznalokController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public FelhasznalokController(EsemenytarContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Felhasznalok>> GetFelhasznalok()
        {
            return _context.Felhasznaloks.ToList();
        }

        [HttpGet("plus")]
        public ActionResult<IEnumerable<Felhasznalok>> GetFelhasznalokPlus()
        //felhasznalo_id,felhasznalonev,email,avatar,telefonszam,leiras,letrehozva,varos_id helyett varosnev,like_szamolo
        {
            return Ok(_context.Felhasznaloks.Include(x => x.Varos).Select(x => new { x.FelhasznaloId, x.Felhasznalonev, x.Email, x.Avatar, x.Telefonszam, x.Leiras, x.Letrehozva, x.Varos.TelepulesNev, x.LikeSzamlalo }));
        }

        [HttpGet("{id}")/*, Authorize("Admin")*/]
        public ActionResult<Felhasznalok> GetFelhasznalo(Guid id)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            return felhasznalo;
        }

        [HttpGet("{id}-helytessitett")/*, Authorize("Admin")*/]
        public ActionResult<Felhasznalok> GetFelhasznaloPlus(Guid id)
        {
            var felhasznalo = _context.Felhasznaloks.Include(x => x.Varos).Where(x => x.FelhasznaloId == id).Select(x => new { x.FelhasznaloId, x.Felhasznalonev, x.Email, x.Avatar, x.Telefonszam, x.Leiras, x.Letrehozva, x.Varos.TelepulesNev, x.LikeSzamlalo }).FirstOrDefault();

            if (felhasznalo == null)
            {
                return NotFound();
            }

            return Ok(felhasznalo);
        }

        [HttpPost]
        public ActionResult<FelhasznalokDto> CreateFelhasznalo(FelhasznalokDto felhasznaloDto)
        {
            var felhasznalo = new Felhasznalok
            {
                LikeSzamlalo = felhasznaloDto.LikeSzamlalo,
            };

            _context.Felhasznaloks.Add(felhasznalo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetFelhasznalo), new { id = felhasznalo.FelhasznaloId }, felhasznalo);
        }

        [HttpPut("{id}"),  Authorize("Admin")]
        public IActionResult UpdateFelhasznalo(Guid id, Felhasznalok updatedFelhasznalo)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            felhasznalo.Felhasznalonev = updatedFelhasznalo.Felhasznalonev;
            felhasznalo.Email = updatedFelhasznalo.Email;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}-bovitett")]
        public IActionResult UpdateFelhasznalo(Guid id, FelhasznalokDto updatedFelhasznalo)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            felhasznalo.Felhasznalonev = updatedFelhasznalo.Felhasznalonev;
            felhasznalo.JelszoHash = updatedFelhasznalo.JelszoHash;
            felhasznalo.Salt = updatedFelhasznalo.Salt;
            felhasznalo.Email = updatedFelhasznalo.Email;
            felhasznalo.Avatar = updatedFelhasznalo.Avatar;
            felhasznalo.Telefonszam = updatedFelhasznalo.Telefonszam;
            felhasznalo.Leiras = updatedFelhasznalo.Leiras;
            felhasznalo.Letrehozva = updatedFelhasznalo.Letrehozva;
            felhasznalo.SzerepId = updatedFelhasznalo.SzerepId;
            felhasznalo.VarosId = updatedFelhasznalo.VarosId;
            felhasznalo.LikeSzamlalo = updatedFelhasznalo.LikeSzamlalo;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize("Admin")]
        public IActionResult DeleteFelhasznalo(Guid id)
        {
            var felhasznalo = _context.Felhasznaloks.Find(id);

            if (felhasznalo == null)
            {
                return NotFound();
            }

            _context.Felhasznaloks.Remove(felhasznalo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
