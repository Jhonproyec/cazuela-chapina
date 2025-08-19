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
using Microsoft.CodeAnalysis;

namespace cazuela_chapina_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellsController : ControllerBase
    {
        private readonly AppDbC _context;

        public SellsController(AppDbC context)
        {
            _context = context;
        }

        // GET: api/Sells
        [HttpGet]
        public async Task<ActionResult<DashboardReport>> Getsell()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var firstDayOfMonth = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Total ventas diarias
            var totalVentasDiarias = await _context.sell
                .Where(s => s.DateSell == today)
                .SumAsync(s => s.Total);

            // Total ventas mensuales
            var totalVentasMensuales = await _context.sell
                .Where(s => s.DateSell >= firstDayOfMonth)
                .SumAsync(s => s.Total);

            // Tamal más y menos vendido por tipo de masa
            var tamalesPorMasa = await _context.sellDetails
                .Where(sd => sd.MasaId != null)
                .GroupBy(sd => sd.MasaId)
                .Select(g => new { MasaId = g.Key, Cantidad = g.Sum(sd => sd.Cantidad ) })
                .OrderByDescending(g => g.Cantidad)
                .ToListAsync();

            var tamalMasVendido = tamalesPorMasa.FirstOrDefault();
            var tamalMenosVendido = tamalesPorMasa.LastOrDefault();

            var nombreTamalMas = tamalMasVendido != null
                ? (await _context.inventory.FindAsync(tamalMasVendido.MasaId))?.Name
                : "N/A";

            var nombreTamalMenos = tamalMenosVendido != null
                ? (await _context.inventory.FindAsync(tamalMenosVendido.MasaId))?.Name
                : "N/A";

            // Bebida más vendida
            var bebidaMasVendida = await _context.sellDetails
                .Where(sd => sd.BebidaId != null)
                .GroupBy(sd => sd.BebidaId)
                .Select(g => new { BebidaId = g.Key, Cantidad = g.Sum(sd => sd.Cantidad) })
                .OrderByDescending(g => g.Cantidad)
                .FirstOrDefaultAsync();

            var nombreBebida = bebidaMasVendida != null
                ? (await _context.inventory.FindAsync(bebidaMasVendida.BebidaId))?.Name
                : "N/A";

            // Bebidas vendidas por horario
            var bebidasPorHorario = _context.sellDetails
                .Include(sd => sd.Sell)
                .Where(sd => sd.BebidaId != null && sd.Sell != null)
                .AsEnumerable()
                .GroupBy(sd => sd.Sell!.DateSell.ToDateTime(TimeOnly.MinValue).Hour / 6)
                .Select(g => new
                {
                    Horario = $"{g.Key * 6}-{g.Key * 6 + 5}h",
                    Cantidad = g.Sum(sd => sd.QuantityBebida ?? 0)
                })
                .ToList();

            var bebidasPorHorarioDict = bebidasPorHorario.ToDictionary(b => b.Horario, b => b.Cantidad);

            // Picantes vs no picantes
            var picantes = await _context.sellDetails.CountAsync(sd => sd.PicanteId != null);
            var noPicantes = await _context.sellDetails.CountAsync(sd => sd.PicanteId == null);

            // Mermas (ejemplo: cantidad de tamales con QuantityTamal menor a lo esperado o nula)
            var mermas = await _context.sellDetails
                .Where(sd => sd.QuantityTamal == null || sd.QuantityTamal < 1)
                .SumAsync(sd => sd.QuantityTamal ?? 0);

            var report = new DashboardReport
            {
                TotalVentasDiarias = totalVentasDiarias,
                TotalVentasMensuales = totalVentasMensuales,
                TamalMasVendido = nombreTamalMas,
                CantidadTamalMasVendido = tamalMasVendido?.Cantidad ?? 0,
                TamalMenosVendido = nombreTamalMenos,
                CantidadTamalMenosVendido = tamalMenosVendido?.Cantidad ?? 0,
                BebidaMasVendida = nombreBebida,
                BebidasPorHorario = bebidasPorHorarioDict,
                Picantes = picantes,
                NoPicantes = noPicantes,
                Mermas = mermas
            };

            return Ok(report);
        }


        // GET: api/Sells/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sell>> GetSell(int id)
        {
            var sell = await _context.sell.FindAsync(id);

            if (sell == null)
            {
                return NotFound();
            }

            return sell;
        }

        // PUT: api/Sells/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSell(int id, Sell sell)
        {
            if (id != sell.SellId)
            {
                return BadRequest();
            }

            _context.Entry(sell).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellExists(id))
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

        // POST: api/Sells
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostSale([FromBody] List<StoreCreateDto> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
                return BadRequest("No hay items en el carrito");

            var sell = new Sell
            {
                DateSell = DateOnly.FromDateTime(DateTime.Now),
                Total = (decimal)cartItems.Sum(x => x.Price * x.Cantidad)
            };

            _context.sell.Add(sell);
            await _context.SaveChangesAsync();

            // Guardar detalles
            foreach (var item in cartItems)
            {
                var detail = new SellDetails
                {
                    SellId = sell.SellId,
                    Cantidad = item.Cantidad,
                    ComboId = item.ComboId,
                    BebidaId = item.IdBebida,
                    EndulzarId = item.IdEndulzar,
                    EnvolturaId = item.IdEnvoltura,
                    MasaId = item.IdMasa,
                    PicanteId = item.IdPicante,
                    RellenoId = item.IdRelleno,
                    Malvavisco = item.Malvavisco,
                    Presentation = item.Presentation,
                    Price = (decimal)item.Price,
                    QuantityBebida = item.QuantityBebida,
                    QuantityTamal = item.QuantityTamal,
                    Topping = item.Topping,
                    ProductId = item.ProductId
                };
                var masa = await _context.inventory.FindAsync(item.IdMasa);
                var relleno = await _context.inventory.FindAsync(item.IdRelleno);
                var envoltura = await _context.inventory.FindAsync(item.IdEnvoltura);
                var picante = await _context.inventory.FindAsync(item.IdPicante);
                var bebida = await _context.inventory.FindAsync(item.IdBebida);

                if (masa != null) masa.Stock -= item.Cantidad;
                if (relleno != null) relleno.Stock -= item.Cantidad;
                if (envoltura != null) envoltura.Stock -= item.Cantidad;
                if (picante != null) picante.Stock -= item.Cantidad;
                if (bebida != null) bebida.Stock -= (float)item.QuantityBebida;
                _context.sellDetails.Add(detail);
            }



            await _context.SaveChangesAsync();

            return Ok(new { SellId = sell.SellId });
        }

        // DELETE: api/Sells/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSell(int id)
        {
            var sell = await _context.sell.FindAsync(id);
            if (sell == null)
            {
                return NotFound();
            }

            _context.sell.Remove(sell);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellExists(int id)
        {
            return _context.sell.Any(e => e.SellId == id);
        }
    }
}
