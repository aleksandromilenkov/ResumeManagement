using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeManagementAPI.DTOs;
using ResumeManagementAPI.DTOs.JobDTOs;
using ResumeManagementAPI.Enums;
using ResumeManagementAPI.Interface;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobController(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        // GET: api/job
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobs = await _jobRepository.GetAllAsync();
            return Ok(jobs);
        }

        // GET: api/job/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _jobRepository.FindByIdAsync(id);
            if (job == null)
                return NotFound(new { message = "Job not found." });

            return Ok(job);
        }

        // POST: api/job
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobCreateDTO jobCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Convert the string to the corresponding enum value
            if (!Enum.TryParse(jobCreateDTO.Level, out LevelEnum level))
            {
                return BadRequest(new { message = "Invalid level value. It must be 'Junior', 'Mid', or 'Senior'." });
            }

            // Map the DTO to the Job entity
            var job = _mapper.Map<Job>(jobCreateDTO);
            job.Level = level; // Assign the correct enum value after conversion
            var response = await _jobRepository.CreateAsync(job);
            if (!response.Flag)
                return BadRequest(new { message = response.Message });

            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        // PUT: api/job/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobDTO jobDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingJob = await _jobRepository.FindByIdAsync(id);
            if (existingJob == null)
                return NotFound(new { message = "Job not found." });

            if (!Enum.TryParse(jobDTO.Level, out LevelEnum level))
            {
                return BadRequest(new { message = "Invalid level value. It must be 'Junior', 'Mid', or 'Senior'." });
            }

            jobDTO.Id = id; // Ensure the ID remains the same
            var job = _mapper.Map<Job>(jobDTO);
            job.Level = level; // Assign correct enum value after conversion
            var response = await _jobRepository.UpdateAsync(job);
            if (!response.Flag)
                return BadRequest(new { message = response.Message });

            return Ok(new { message = "Job updated successfully." });
        }

        // DELETE: api/job/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _jobRepository.FindByIdAsync(id);
            if (job == null)
                return NotFound(new { message = "Job not found." });

            var response = await _jobRepository.DeleteAsync(job);
            if (!response.Flag)
                return BadRequest(new { message = response.Message });

            return Ok(new { message = "Job deleted successfully." });
        }
    }
}
