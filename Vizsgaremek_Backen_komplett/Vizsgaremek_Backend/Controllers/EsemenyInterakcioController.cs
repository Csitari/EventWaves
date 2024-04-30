using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EsemenyInterakcioController : ControllerBase
    {
        private readonly EsemenytarContext _dbContext;

        public EsemenyInterakcioController(EsemenytarContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("felhasznalo-es-esemeny-alapjan")]
        public IActionResult GetEsemenyInterakciokByUserAndEvent(Guid? userId, Guid eventId)
        {
            IQueryable<EsemenyInterakcio> query = _dbContext.EsemenyInterakcios;

            if (userId != Guid.Empty)
            {
                query = query.Where(x => x.FelhasznaloId == userId);
            }

            if (eventId != Guid.Empty)
            {
                query = query.Where(x => x.EsemenyId == eventId);
            }

            var esemenyInterakciok = query.ToList();

            return Ok(esemenyInterakciok);
        }


        // GET: api/EsemenyInterakcio
        [HttpGet]
        public IActionResult GetEsemenyInterakciok()
        {
            var esemenyInterakciok = _dbContext.EsemenyInterakcios.ToList();
            return Ok(esemenyInterakciok);
        }

        // POST: api/EsemenyInterakcio
        [HttpPost]
        public IActionResult CreateEsemenyInterakcio(EsemenyInterakcioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var esemenyInterakcio = new EsemenyInterakcio
            {
                FelhasznaloId = dto.FelhasznaloId,
                EsemenyId = dto.EsemenyId,
                JelentkezettE = dto.JelentkezettE,
                KedveltE = dto.KedveltE,
                MentettE = dto.MentettE,
                JelentkezesDatum = dto.JelentkezesDatum
            };

            _dbContext.EsemenyInterakcios.Add(esemenyInterakcio);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetEsemenyInterakciok), new { id = esemenyInterakcio.InterakcioId }, esemenyInterakcio);
        }

        [HttpPut("ById/{interakcioId}")]
        public IActionResult UpdateEsemenyInterakcio(int interakcioId, EsemenyInterakcioDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var esemenyInterakcio = _dbContext.EsemenyInterakcios.FirstOrDefault(e => e.InterakcioId == interakcioId);
            if (esemenyInterakcio == null)
            {
                return NotFound();
            }

            esemenyInterakcio.FelhasznaloId = dto.FelhasznaloId;
            esemenyInterakcio.EsemenyId = dto.EsemenyId;
            esemenyInterakcio.JelentkezettE = dto.JelentkezettE;
            esemenyInterakcio.KedveltE = dto.KedveltE;
            esemenyInterakcio.MentettE = dto.MentettE;
            esemenyInterakcio.JelentkezesDatum = dto.JelentkezesDatum;

            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}

