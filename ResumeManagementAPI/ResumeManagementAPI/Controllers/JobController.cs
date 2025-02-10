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

            // Map the DTO to the Job entity
            var job = _mapper.Map<Job>(jobCreateDTO);
            var response = await _jobRepository.CreateAsync(job);
            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
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


            jobDTO.Id = id; // Ensure the ID remains the same
            var job = _mapper.Map<Job>(jobDTO);
            var response = await _jobRepository.UpdateAsync(job);
            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }

        // DELETE: api/job/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _jobRepository.FindByIdAsync(id);
            if (job == null)
                return NotFound(new { message = "Job not found." });

            var response = await _jobRepository.DeleteAsync(job);
            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }
    }
}
