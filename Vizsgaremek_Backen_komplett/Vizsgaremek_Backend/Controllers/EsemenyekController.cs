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
    public class EsemenyekController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public EsemenyekController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Esemenyek>> GetEsemenyek()
        {
            return _context.Esemenyeks.ToList();
        }

        [HttpGet("Telepulesek/{telepulesNev}")]
        public ActionResult<IEnumerable<Esemenyek>> GetEsemenyekByTelepules(string telepulesNev)
        {
            var esemenyek = _context.Esemenyeks
                                    .Include(x => x.Kategoria)
                                    .Include(x => x.Szervezo)
                                    .Include(x => x.Varos)
                                    .Where(x => x.Varos.TelepulesNev == telepulesNev)
                                    .Select(x => new {
                                        x.EsemenyId,
                                        x.Cim,
                                        x.Kategoria.KategoriaNev,
                                        x.Varos.TelepulesNev,
                                        x.Leiras,
                                        x.Idopont,
                                        x.Szervezo.Felhasznalonev,
                                        x.Korhatar,
                                        x.Letrehozva,
                                        x.LikeSzamlalo,
                                        x.DislikeSzamlalo
                                    })
                                    .ToList();

            if (esemenyek == null || esemenyek.Count == 0)
            {
                return NotFound();
            }

            return Ok(esemenyek);
        }

        [HttpGet("EsemenyekByMegye/{megyeNev}")]
        public ActionResult<IEnumerable<Esemenyek>> GetEsemenyekByMegye(string megyeNev)
        {
            var esemenyek = _context.Esemenyeks
                                    .Include(x => x.Kategoria)
                                    .Include(x => x.Szervezo)
                                    .Include(x => x.Varos)
                                    .Where(x => x.Varos.Megye.Megyenev == megyeNev)
                                    .Select(x => new {
                                        x.EsemenyId,
                                        x.Cim,
                                        x.Kategoria.KategoriaNev,
                                        x.Varos.TelepulesNev,
                                        x.Leiras,
                                        x.Idopont,
                                        x.Szervezo.Felhasznalonev,
                                        x.Korhatar,
                                        x.Letrehozva,
                                        x.LikeSzamlalo,
                                        x.DislikeSzamlalo
                                    })
                                    .ToList();

            if (esemenyek == null || esemenyek.Count == 0)
            {
                return NotFound();
            }

            return Ok(esemenyek);
        }


        [HttpGet("plus")]
        public ActionResult<IEnumerable<Esemenyek>> GetEsemenyekPlus()
        {
            return Ok(_context.Esemenyeks.Include(x => x.Kategoria).Include(x => x.Szervezo).Include(x => x.Varos).Select(x => new { x.EsemenyId, x.Cim, x.Kategoria.KategoriaNev, x.Varos.TelepulesNev, x.Leiras, x.Idopont, x.Szervezo.Felhasznalonev, x.Korhatar, x.Letrehozva, x.LikeSzamlalo, x.DislikeSzamlalo }));
        }

        [HttpGet("{id}")]
        public ActionResult<Esemenyek> GetEsemeny(Guid id)
        {
            var esemeny = _context.Esemenyeks.Find(id);

            if (esemeny == null)
            {
                return NotFound();
            }

            return esemeny;
        }

        [HttpGet("{id}-feletteshelyettes")]
        public ActionResult<Esemenyek> GetEsemenyPlus(Guid id)
        {
            var esemeny = _context.Esemenyeks.Include(x => x.Kategoria).Include(x => x.Szervezo).Include(x => x.Varos).Where(x => x.EsemenyId == id).Select(x => new { x.EsemenyId, x.Cim, x.Kategoria.KategoriaNev, x.Varos.TelepulesNev, x.BoritoKep, x.Leiras, x.Idopont, x.Szervezo.Felhasznalonev, x.Korhatar, x.Letrehozva, x.LikeSzamlalo, x.DislikeSzamlalo }).FirstOrDefault();

            if (esemeny == null)
            {
                return NotFound();
            }

            return Ok(esemeny);
        }

        [HttpPost]
        public IActionResult CreateEsemeny(EsemenyekDto esemenyCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var esemeny = new Esemenyek
            {
                Cim = esemenyCreateDTO.Cim,
                BoritoKep = esemenyCreateDTO.BoritoKep,
                Leiras = esemenyCreateDTO.Leiras,
                KategoriaId = esemenyCreateDTO.KategoriaId,
                Idopont = esemenyCreateDTO.Idopont,
                VarosId = esemenyCreateDTO.VarosId,
                SzervezoId = esemenyCreateDTO.SzervezoId,
                Korhatar = esemenyCreateDTO.Korhatar,
                Statusz = esemenyCreateDTO.Statusz,
                Letrehozva = DateTime.UtcNow,
                LikeSzamlalo = 0,
                DislikeSzamlalo = 0
            };

            _context.Esemenyeks.Add(esemeny);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetEsemeny), new { id = esemeny.EsemenyId }, esemeny);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateEsemeny(Guid id, EsemenyekDto updatedEsemeny)
        {
            var esemeny = _context.Esemenyeks.Find(id);

            if (esemeny == null)
            {
                return NotFound();
            }

            esemeny.Cim = updatedEsemeny.Cim;
            esemeny.BoritoKep = updatedEsemeny.BoritoKep;
            esemeny.Leiras = updatedEsemeny.Leiras;
            esemeny.KategoriaId = updatedEsemeny.KategoriaId;
            esemeny.Idopont = updatedEsemeny.Idopont;
            esemeny.SzervezoId = updatedEsemeny.SzervezoId;
            esemeny.Korhatar = updatedEsemeny.Korhatar;
            esemeny.Statusz = updatedEsemeny.Statusz;
            esemeny.Letrehozva = updatedEsemeny.Letrehozva;
            esemeny.LikeSzamlalo = updatedEsemeny.LikeSzamlalo;
            esemeny.DislikeSzamlalo = updatedEsemeny.DislikeSzamlalo;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEsemeny(Guid id)
        {
            var esemeny = _context.Esemenyeks.Find(id);

            if (esemeny == null)
            {
                return NotFound();
            }

            _context.Esemenyeks.Remove(esemeny);
            _context.SaveChanges();

            return NoContent();
        }
    }
}