using EventFlowerExchange.Repositories.Entities;
using EventFlowerExchange.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventFlowerExchange.WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;

        public FlowerController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlower(int id)
        {
            var flower = await _flowerService.GetFlowerByIdAsync(id);
            if (flower == null) return NotFound();
            return Ok(flower);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFlowers()
        {
            var flowers = await _flowerService.GetAllFlowersAsync();
            return Ok(flowers);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlower([FromBody] Flower flower)
        {
            await _flowerService.AddFlowerAsync(flower);
            return CreatedAtAction(nameof(GetFlower), new { id = flower.Id }, flower);
        }
    }

}
