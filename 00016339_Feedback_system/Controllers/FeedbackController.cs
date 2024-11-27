using _00016339_Feedback_system.DTO;
using _00016339_Feedback_system.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _00016339_Feedback_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IRepository<Feedback> _feedbackRepository;
        private readonly IRepository<Sender> _senderRepository;
        private readonly IMapper _mapper;
        public FeedbackController(IRepository<Feedback> feedbackRepository, IMapper mapper, IRepository<Sender> senderRepository)
        {
            _feedbackRepository = feedbackRepository;
            _senderRepository = senderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FeedbackDto>> GetAll()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            var dtos = new List<FeedbackDto>();
            foreach (var item in feedbacks)
            {
                var sender = await _senderRepository.GetByIdAsync(item.SenderId);
                var dto = new FeedbackDto();
                dto.Id = item.Id;
                dto.Title = item.Title;
                dto.Description = item.Description;
                dto.Sender = sender;
                dto.DateCreated = item.DateCreated;
                dtos.Add(dto);
            }
            return dtos;
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(FeedbackDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetByID(int id)
        {
            var resultedFeedback = await _feedbackRepository.GetByIdAsync(id);
            FeedbackDto? dto = null;
            if (resultedFeedback != null)
            {
                dto = new FeedbackDto();
                var sender = await _senderRepository.GetByIdAsync(resultedFeedback.SenderId);
                dto.Id = resultedFeedback.Id;
                dto.Title = resultedFeedback.Title;
                dto.Description = resultedFeedback.Description;
                dto.Sender = sender;
                dto.DateCreated = resultedFeedback.DateCreated;
            }
            return dto == null ? NotFound() : Ok(dto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> Create(Feedback items)
        {
            await _feedbackRepository.AddAsync(items);
            var sender = await _senderRepository.GetByIdAsync(items.SenderId);
            var dto = new FeedbackDto();
            dto.Id = items.Id;
            dto.Title = items.Title;
            dto.Description = items.Description;
            dto.Sender = sender;
            dto.DateCreated = items.DateCreated;
            return CreatedAtAction(nameof(GetByID), new { id = items.Id }, items);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Feedback items)
        {
            if (id != items.Id) return BadRequest();
            await _feedbackRepository.UpdateAsync(items);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _feedbackRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
