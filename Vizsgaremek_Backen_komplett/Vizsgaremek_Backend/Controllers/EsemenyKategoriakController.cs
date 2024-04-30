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
    public class EsemenyKategoriakController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public EsemenyKategoriakController(EsemenytarContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<EsemenyKategoriak>> GetEsemenyKategoriak()
        {
            return _context.EsemenyKategoriaks.ToList();
        }

        
        [HttpGet("{id}"), Authorize("Admin")]
        public ActionResult<EsemenyKategoriak> GetEsemenyKategoria(int id)
        {
            var esemenyKategoria = _context.EsemenyKategoriaks.Find(id);

            if (esemenyKategoria == null)
            {
                return NotFound();
            }

            return esemenyKategoria;
        }

        [HttpPost, Authorize("Admin")]
        public ActionResult<EsemenyKategoriak> CreateEsemenyKategoria(EsemenyKategoriak esemenyKategoria)
        {
            _context.EsemenyKategoriaks.Add(esemenyKategoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEsemenyKategoria), new { id = esemenyKategoria.KategoriaId }, esemenyKategoria);
        }

        [HttpPut("{id}"), Authorize("Admin")]
        public IActionResult UpdateEsemenyKategoria(int id, EsemenyKategoriak updatedEsemenyKategoria)
        {
            var esemenyKategoria = _context.EsemenyKategoriaks.Find(id);

            if (esemenyKategoria == null)
            {
                return NotFound();
            }

            esemenyKategoria.KategoriaNev = updatedEsemenyKategoria.KategoriaNev;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize("Admin")]
        public IActionResult DeleteEsemenyKategoria(int id)
        {
            var esemenyKategoria = _context.EsemenyKategoriaks.Find(id);

            if (esemenyKategoria == null)
            {
                return NotFound();
            }

            _context.EsemenyKategoriaks.Remove(esemenyKategoria);
            _context.SaveChanges();

            return NoContent();
        }
    }
}