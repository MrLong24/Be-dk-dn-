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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFlower(int id, [FromBody] Flower updatedFlower)
        {
            if (updatedFlower == null)
            {
                return BadRequest(new { message = "Flower data is missing." });
            }

            if (id != updatedFlower.Id)
            {
                return BadRequest(new { message = "ID in URL does not match ID in the request body." });
            }

            try
            {
                await _flowerService.UpdateFlowerAsync(updatedFlower);
                return Ok(new { message = "Flower updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the flower.", error = ex.Message });
            }
        }



        // API to delete flower by Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlower(int id)
        {
            try
            {
                await _flowerService.DeleteFlowerAsync(id);
                return Ok(new { message = "Flower deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the flower.", error = ex.Message });
            }
        }
    }

}
