using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cazuela_chapina_api.Context;
using cazuela_chapina_api.Models;
using cazuela_chapina_api.DTO;

namespace cazuela_chapina_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboesController : ControllerBase
    {
        private readonly AppDbC _context;

        public ComboesController(AppDbC context)
        {
            _context = context;
        }

        // GET: api/Comboes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combo>>> Getcombo()
        {
            return await _context.combo.ToListAsync();
        }

        // GET: api/Comboes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetCombo(int id)
        {
            var combo = await _context.combo.FindAsync(id);

            if (combo == null)
            {
                return NotFound();
            }

            return combo;
        }

        // PUT: api/Comboes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombo(int id, ComboCreateDto comboDto)
        {
            // Buscar la entidad existente
            var combo = await _context.combo.FindAsync(id);
            if (combo == null)
            {
                return NotFound();
            }

            // Mapear los cambios del DTO a la entidad
            combo.Name = comboDto.Name;
            combo.Description = comboDto.Description;
            combo.IdMasa = comboDto.IdMasa;
            combo.QuantityTamal = comboDto.QuantityTamal;
            combo.IdRelleno = comboDto.IdRelleno;
            combo.IdEnvoltura = comboDto.IdEnvoltura;
            combo.IdPicante = comboDto.IdPicante;
            combo.IdBebida = comboDto.IdBebida;
            combo.Presentation = comboDto.Presentation;
            combo.QuantityBebida = comboDto.QuantityBebida;
            combo.Price = comboDto.Price;

            // Marcar como modificado y guardar cambios
            _context.Entry(combo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComboExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Comboes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Combo>> PostCombo(ComboCreateDto comboDto)
        {
            // Mapear DTO a entidad
            var combo = new Combo
            {
                Name = comboDto.Name,
                Description = comboDto.Description,
                IdMasa = comboDto.IdMasa,
                QuantityTamal = comboDto.QuantityTamal,
                IdRelleno = comboDto.IdRelleno,
                IdEnvoltura = comboDto.IdEnvoltura,
                IdPicante = comboDto.IdPicante,
                IdBebida = comboDto.IdBebida,
                Presentation = comboDto.Presentation,
                QuantityBebida = comboDto.QuantityBebida,
                Price = comboDto.Price
            };
                //var masa = await _context.inventory.FindAsync(combo.IdMasa);
                //var relleno = await _context.inventory.FindAsync(combo.IdRelleno);
                //var envoltura = await _context.inventory.FindAsync(combo.IdEnvoltura);
                //var picante = await _context.inventory.FindAsync(combo.IdPicante);
                //var bebida = await _context.inventory.FindAsync(combo.IdBebida);

                //if (masa != null) masa.Stock -= combo.QuantityTamal;
                //if (relleno != null) relleno.Stock -= combo.QuantityTamal;
                //if (envoltura != null) envoltura.Stock -= combo.QuantityTamal;
                //if (picante != null) picante.Stock -= combo.QuantityTamal;
                //if (bebida != null) bebida.Stock -= combo.QuantityBebida;


            _context.combo.Add(combo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCombo", new { id = combo.ComboId }, combo);
        }


        // DELETE: api/Comboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombo(int id)
        {
            var combo = await _context.combo.FindAsync(id);
            if (combo == null)
            {
                return NotFound();
            }

            _context.combo.Remove(combo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComboExists(int id)
        {
            return _context.combo.Any(e => e.ComboId == id);
        }
    }
}
