using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vizsgaremek_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Vizsgaremek_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsemenyHozzaszolasokController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public EsemenyHozzaszolasokController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EsemenyHozzaszolasok>>> GetAllHozzaszolas()
        {
            return await _context.EsemenyHozzaszolasoks.ToListAsync();
        }

        [HttpGet("plus")]
        public ActionResult<EsemenyHozzaszolasok> GetAllHozzaszolasPlus()
        //hozzaszolas_id,esemeny_id,hozzaszolo_id helyett ffelhasznalonev,hozzaszolas_szoveg,letrehozva
        {
            return Ok(_context.EsemenyHozzaszolasoks.Include(x => x.Esemeny).Include(x => x.Hozzaszolo).Select(x => new { x.HozzaszolasId, x.EsemenyId, x.HozzaszoloId, Felhasznalonev = x.Hozzaszolo.Felhasznalonev, x.HozzaszolasSzoveg, x.Letrehozva }).ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EsemenyHozzaszolasok>> GetHozzaszolas(Guid id)
        {
            var item = await _context.EsemenyHozzaszolasoks.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("{id}-helyetesitett")]
        public async Task<ActionResult<EsemenyHozzaszolasok>> GetHozzaszolasPlus(Guid id)
        {
            var hozzaszolas = await _context.EsemenyHozzaszolasoks.Include(x => x.Esemeny).Include(x => x.Hozzaszolo).Select(x => new { x.HozzaszolasId, x.EsemenyId, x.HozzaszoloId, Felhasznalonev = x.Hozzaszolo.Felhasznalonev, x.HozzaszolasSzoveg, x.Letrehozva }).FirstOrDefaultAsync(x => x.HozzaszolasId == id);
            if (hozzaszolas == null)
            {
                return NotFound();
            }
            return Ok(hozzaszolas);
        }

        [HttpPost]
        public async Task<ActionResult<EsemenyHozzaszolasok>> Create(EsemenyHozzaszolasokDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hozzaszolas = new EsemenyHozzaszolasok
            {
                EsemenyId = dto.EsemenyId,
                HozzaszoloId = dto.HozzaszoloId,
                HozzaszolasSzoveg = dto.HozzaszolasSzoveg,
                Letrehozva = dto.Letrehozva
            };

            _context.EsemenyHozzaszolasoks.Add(hozzaszolas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHozzaszolas), new { id = hozzaszolas.HozzaszolasId }, hozzaszolas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, EsemenyHozzaszolasok item)
        {
            if (id != item.HozzaszolasId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.EsemenyHozzaszolasoks.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.EsemenyHozzaszolasoks.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

