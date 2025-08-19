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
    public class InventoriesController : ControllerBase
    {
        private readonly AppDbC _context;

        public InventoriesController(AppDbC context)
        {
            _context = context;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryReadDto>>> Getinventory()
        {

            var inventories = await _context.inventory
             .Include(i => i.Supplier)
             .Include(i => i.UnitMeasurement)
             .Select(i => new InventoryReadDto
             {
                 InventoryId = i.InventoryId,
                 Name = i.Name,
                 Stock = i.Stock,
                 Mermas = i.Mermas,
                 QuantityAprox = i.QuantityAprox,
                 SupplierId = i.SupplierId,
                 SupplierName = i.Supplier.Name,
                 UnitMeasurementId = i.UnitMeasurementId,
                 UnitMeasurementName = i.UnitMeasurement.Name,
                 Type = i.Type
             })
             .ToListAsync();

            return Ok(inventories);
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
            var inventory = await _context.inventory.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        // PUT: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryReadDto>> PutInventory(int id, InventoryCreateDto dto)
        {
            var inventory = await _context.inventory.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            // Actualizamos los campos
            inventory.Name = dto.Name;
            inventory.SupplierId = dto.SupplierId;
            inventory.Mermas = dto.Mermas;
            inventory.UnitMeasurementId = dto.UnitMeasurementId;
            inventory.QuantityAprox = dto.QuantityAprox;
            inventory.Type = dto.Type;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Volvemos a consultar con Include para devolver datos completos
            var updatedInventory = await _context.inventory
                .Include(i => i.Supplier)
                .Include(i => i.UnitMeasurement)
                .Select(i => new InventoryReadDto
                {
                    InventoryId = i.InventoryId,
                    Name = i.Name,
                    Stock = i.Stock,
                    Mermas = i.Mermas,
                    QuantityAprox = i.QuantityAprox,
                    SupplierId = i.SupplierId,
                    SupplierName = i.Supplier.Name,
                    UnitMeasurementId = i.UnitMeasurementId,
                    UnitMeasurementName = i.UnitMeasurement.Name,
                    Type = i.Type
                })
                .FirstOrDefaultAsync(i => i.InventoryId == id);

            return Ok(updatedInventory);
        }


        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InventoryReadDto>> PostInventory(InventoryCreateDto dto)
        {
            var inventory = new Inventory
            {
                Name = dto.Name,
                SupplierId = dto.SupplierId,
                Stock = 500,
                Mermas = dto.Mermas,
                UnitMeasurementId = dto.UnitMeasurementId,
                QuantityAprox = dto.QuantityAprox,
                Type = dto.Type
            };

            _context.inventory.Add(inventory);
            await _context.SaveChangesAsync();

            var savedInventory = await _context.inventory
                .Include(i => i.Supplier)
                .Include(i => i.UnitMeasurement)
                .FirstOrDefaultAsync(i => i.InventoryId == inventory.InventoryId);

            var result = new InventoryReadDto
            {
                InventoryId = savedInventory.InventoryId,
                Name = savedInventory.Name,
                Stock = savedInventory.Stock,
                Mermas = savedInventory.Mermas,
                QuantityAprox = savedInventory.QuantityAprox,
                SupplierId = savedInventory.SupplierId,
                SupplierName = savedInventory.Supplier.Name,
                UnitMeasurementId = savedInventory.UnitMeasurementId,
                UnitMeasurementName = savedInventory.UnitMeasurement.Name,
                Type = savedInventory.Type
            };

            return CreatedAtAction("GetInventory", new { id = result.InventoryId }, result);
        }


        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _context.inventory.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.inventory.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return _context.inventory.Any(e => e.InventoryId == id);
        }
    }
}
