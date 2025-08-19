using cazuela_chapina_api.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using cazuela_chapina_api.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace cazuela_chapina_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatTiendaController : ControllerBase
    {
        private readonly AppDbC _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ChatTiendaController(AppDbC context, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
            _config = config;
        }

        [HttpPost("ask")]
        public async Task<IActionResult> Ask([FromBody] UserQuestionDto request)
        {
            // 1. Obtenemos los datos de la tienda
            var snapshot = new StoreSnapshotDto
            {
                Products = await _context.product
                    .Select(p => new ProductDto { ProductId = p.ProductId, Name = p.Name })
                    .ToListAsync(),

                Sells = await _context.sell
                    .Select(s => new SellDto { SellId = s.SellId, DateSell = s.DateSell, Total = s.Total })
                    .ToListAsync(),

                SellDetails = await _context.sellDetails
                    .Select(sd => new SellDetailDto { SellDetailsId = sd.SellDetailsId, SellId = sd.SellId, ProductId = sd.ProductId, Cantidad = sd.Cantidad, Price = sd.Price })
                    .ToListAsync(),

                Combos = await _context.combo
                    .Select(c => new ComboDto { ComboId = c.ComboId, Name = c.Name })
                    .ToListAsync(),

                Inventory = await _context.inventory
                    .Select(i => new InventoryDto { InventoryId = i.InventoryId, Name = i.Name, Stock = (int)i.Stock })
                    .ToListAsync(),

                Suppliers = await _context.suppliers
                    .Select(su => new SupplierDto { SupplierId = su.supplierId, Name = su.Name })
                    .ToListAsync()
            };

            // 2. Construir el prompt
            var prompt = $@"
Eres un asistente que analiza datos de una tienda.
Pregunta del usuario: {request.Question}

Datos actuales de la tienda:
{JsonSerializer.Serialize(snapshot)}

Responde de manera clara y resumida para el usuario final.
";

            // 3. Llamar a OpenRouter
            var body = new
            {
                model = "gpt-oss-20b", // Modelo gratuito de OpenRouter
                messages = new object[]
                {
                    new { role = "system", content = "Eres un experto en ventas y análisis de datos de tienda." },
                    new { role = "user", content = prompt }
                }
            };

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_config["OpenRouter:ApiKey"]}");

            var response = await _httpClient.PostAsJsonAsync(
                "https://openrouter.ai/api/v1/chat/completions",
                body,
                new JsonSerializerOptions { PropertyNamingPolicy = null }
            );

            if (!response.IsSuccessStatusCode)
                return BadRequest(await response.Content.ReadAsStringAsync());

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            var reply = json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return Ok(new { answer = reply });
        }

        [HttpGet("snapshot")]
        public async Task<ActionResult<StoreSnapshotDto>> GetSnapshot()
        {
            var snapshot = new StoreSnapshotDto
            {
                Products = await _context.product
                    .Select(p => new ProductDto { ProductId = p.ProductId, Name = p.Name })
                    .ToListAsync(),

                Sells = await _context.sell
                    .Select(s => new SellDto { SellId = s.SellId, DateSell = s.DateSell, Total = s.Total })
                    .ToListAsync(),

                SellDetails = await _context.sellDetails
                    .Select(sd => new SellDetailDto
                    {
                        SellDetailsId = sd.SellDetailsId,
                        SellId = sd.SellId,
                        ProductId = sd.ProductId,
                        Cantidad = sd.Cantidad,
                        Price = sd.Price
                    }).ToListAsync(),

                Combos = await _context.combo
                    .Select(c => new ComboDto { ComboId = c.ComboId, Name = c.Name })
                    .ToListAsync(),

                Inventory = await _context.inventory
                    .Select(i => new InventoryDto { InventoryId = i.InventoryId, Name = i.Name, Stock = (int)i.Stock })
                    .ToListAsync(),

                Suppliers = await _context.suppliers
                    .Select(su => new SupplierDto { SupplierId = su.supplierId, Name = su.Name })
                    .ToListAsync()
            };

            return Ok(snapshot);
        }
    }

    public class UserQuestionDto
    {
        public string Question { get; set; } = string.Empty;
    }
}
