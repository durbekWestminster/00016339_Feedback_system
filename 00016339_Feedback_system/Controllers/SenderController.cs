using _00016339_Feedback_system.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _00016339_Feedback_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly IRepository<Sender> _senderRepository;
        public SenderController(IRepository<Sender> senderRepository)
        {
            _senderRepository = senderRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Sender>> GetAll() => await _senderRepository.GetAllAsync();

        [HttpGet("id")]
        [ProducesResponseType(typeof(Sender), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetByID(int id)
        {
            var resultedSender = await _senderRepository.GetByIdAsync(id);
            return resultedSender == null ? NotFound() : Ok(resultedSender);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> Create(Sender items)
        {
            await _senderRepository.AddAsync(items);
            return CreatedAtAction(nameof(GetByID), new { id = items.Id }, items);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Sender items)
        {
            if (id != items.Id) return BadRequest();
            await _senderRepository.UpdateAsync(items);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _senderRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
