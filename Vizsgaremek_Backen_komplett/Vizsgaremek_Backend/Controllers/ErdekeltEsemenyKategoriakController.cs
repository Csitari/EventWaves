using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErdekeltEsemenyKategoriakController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public ErdekeltEsemenyKategoriakController(EsemenytarContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ErdekeltEsemenyKategoriak>> GetErdekeltEsemenyKategoriak()
        {
            return _context.ErdekeltEsemenyKategoriaks.ToList();
        }


        [HttpGet("{id}")]
        public ActionResult<ErdekeltEsemenyKategoriak> GetErdekeltEsemenyKategoria(Guid id)
        {
            var erdekeltEsemenyKategoria = _context.ErdekeltEsemenyKategoriaks.Find(id);

            if (erdekeltEsemenyKategoria == null)
            {
                return NotFound();
            }

            return erdekeltEsemenyKategoria;
        }

        [HttpPost]
        public IActionResult CreateErdekeltEsemenyKategoria(ErdekeletEsemenyKategoriakDto dto)
        {
            var newErdekeltEsemenyKategoria = new ErdekeltEsemenyKategoriak
            {
                FelhasznaloId = dto.FelhasznaloId,
                KategoriaId = dto.KategoriaId,
                KategoriaPont = dto.KategoriaPont
            };

            _context.ErdekeltEsemenyKategoriaks.Add(newErdekeltEsemenyKategoria);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetErdekeltEsemenyKategoria), new { id = newErdekeltEsemenyKategoria.FelhasznaloId }, newErdekeltEsemenyKategoria);
        }




        [HttpPut("{id}")]
        public IActionResult UpdateErdekeltEsemenyKategoria(Guid id, ErdekeltEsemenyKategoriak updatedErdekeltEsemenyKategoria)
        {
            var erdekeltEsemenyKategoria = _context.ErdekeltEsemenyKategoriaks.Find(id);

            if (erdekeltEsemenyKategoria == null)
            {
                return NotFound();
            }

            erdekeltEsemenyKategoria.KategoriaId = updatedErdekeltEsemenyKategoria.KategoriaId;
            erdekeltEsemenyKategoria.KategoriaPont = updatedErdekeltEsemenyKategoria.KategoriaPont;
            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteErdekeltEsemenyKategoria(Guid id)
        {
            var erdekeltEsemenyKategoria = _context.ErdekeltEsemenyKategoriaks.Find(id);

            if (erdekeltEsemenyKategoria == null)
            {
                return NotFound();
            }

            _context.ErdekeltEsemenyKategoriaks.Remove(erdekeltEsemenyKategoria);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
