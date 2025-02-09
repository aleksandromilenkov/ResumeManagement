using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeManagementAPI.DTOs;
using ResumeManagementAPI.DTOs.CandidateDTOs;
using ResumeManagementAPI.Interface;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateController(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        // GET: api/candidate
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return Ok(candidates);
        }

        // GET: api/candidate/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateRepository.FindByIdAsync(id);
            if (candidate == null)
                return NotFound(new { message = "Candidate not found." });

            return Ok(candidate);
        }

        // POST: api/candidate
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CandidateCreateDTO candidateCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if the email already exists
            var existingCandidate = await _candidateRepository.GetByAsync(c => c.Email == candidateCreateDTO.Email);
            if (existingCandidate != null)
                return BadRequest(new { message = "Candidate with this email already exists." });

            var candidate = _mapper.Map<Candidate>(candidateCreateDTO);
            var response = await _candidateRepository.CreateAsync(candidate);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }

        // PUT: api/candidate/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CandidateDTO candidateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCandidate = await _candidateRepository.FindByIdAsync(id);
            if (existingCandidate == null)
                return NotFound(new { message = "Candidate not found." });

            candidateDTO.Id = id; // Ensure the ID remains the same
            var candidate = _mapper.Map<Candidate>(candidateDTO);
            var response = await _candidateRepository.UpdateAsync(candidate);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }

        // DELETE: api/candidate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var candidate = await _candidateRepository.FindByIdAsync(id);
            if (candidate == null)
                return NotFound(new { message = "Candidate not found." });

            var response = await _candidateRepository.DeleteAsync(candidate);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }
    }
}
