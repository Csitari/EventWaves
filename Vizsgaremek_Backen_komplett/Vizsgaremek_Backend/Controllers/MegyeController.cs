using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MegyekController : ControllerBase
    {
        private readonly EsemenytarContext _context;

        public MegyekController(EsemenytarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Megyek>> GetAll()
        {
            return Ok(_context.Telepuleseks.Include(x => x.MegyeId).Select(x => new {}));
        }

        [HttpGet("{id}"), /*Authorize("Admin")*/]
        public ActionResult<Megyek> GetById(int id)
        {
            var item = _context.Megyeks.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost, Authorize("Admin")]
        public IActionResult Create(Megyek item)
        {
            _context.Megyeks.Add(item);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = item.MegyeId }, item);
        }
        [HttpPut("{id}"), Authorize("admin")]
        public IActionResult Update(int id, Megyek item)
        {
            if (id != item.MegyeId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}"), Authorize("Admin")]
        public IActionResult Delete(int id)
        {
            var item = _context.Megyeks.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Megyeks.Remove(item);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

