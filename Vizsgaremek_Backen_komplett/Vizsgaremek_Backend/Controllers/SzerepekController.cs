
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SzerepekController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public SzerepekController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize("Admin")]
        public ActionResult<IEnumerable<Szerepek>> GetSzerepek()
        {
            return _context.Szerepeks.ToList();
        }

        [HttpGet("{id}"), Authorize("Admin")]
        public ActionResult<Szerepek> GetSzerep(int id)
        {
            var szerep = _context.Szerepeks.Find(id);

            if (szerep == null)
            {
                return NotFound();
            }

            return szerep;
        }

        [HttpPost, Authorize("Admin")]
        public ActionResult<Szerepek> CreateSzerep(Szerepek szerep)
        {
            _context.Szerepeks.Add(szerep);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSzerep), new { id = szerep.SzerepId }, szerep);
        }

        [HttpPut("{id}"), Authorize("Admin")]
        public IActionResult UpdateSzerep(int id, Szerepek updatedSzerep)
        {
            var szerep = _context.Szerepeks.Find(id);

            if (szerep == null)
            {
                return NotFound();
            }

            szerep.Szerepnev = updatedSzerep.Szerepnev;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize("Admin")]
        public IActionResult DeleteSzerep(int id)
        {
            var szerep = _context.Szerepeks.Find(id);

            if (szerep == null)
            {
                return NotFound();
            }

            _context.Szerepeks.Remove(szerep);
            _context.SaveChanges();

            return NoContent();
        }
    }
}