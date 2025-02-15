using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
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
            var candidatesDTOs = _mapper.Map<IEnumerable<CandidateDTO>>(candidates);
            return Ok(candidatesDTOs);
        }

        // GET: api/candidate/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateRepository.FindByIdAsync(id);
            if (candidate == null)
                return NotFound(new { message = "Candidate not found." });
            var candidateDTO = _mapper.Map<CandidateDTO>(candidate);
            return Ok(candidateDTO);
        }

        // POST: api/candidate
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CandidateCreateDTO candidateCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resume = candidateCreateDTO.Resume;

            // First -> Save File to the Server
            // Then -> Save the URL on the Entity
            var fiveMB = 5 * 1024 * 1024;
            var pdfMimeType = "application/pdf";

            if(resume.Length > fiveMB || resume.ContentType != pdfMimeType)
            {
                return BadRequest("Not valid file type");
            }

            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "resumes");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var resumeURL = Guid.NewGuid().ToString() + ".pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "resumes", resumeURL);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resume.CopyToAsync(stream);
            }
            
            var existingCandidate = await _candidateRepository.GetByAsync(c => c.Email == candidateCreateDTO.Email);
            if (existingCandidate != null)
                return BadRequest(new { message = "Candidate with this email already exists." });

            var candidate = _mapper.Map<Candidate>(candidateCreateDTO);
            candidate.ResumeUrl = resumeURL;
            var response = await _candidateRepository.CreateAsync(candidate);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }

        // PUT: api/candidate/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] CandidateUpdateDTO candidateUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCandidate = await _candidateRepository.FindByIdAsync(id);
            if (existingCandidate == null)
                return NotFound(new { message = "Candidate not found." });

            var resume = candidateUpdateDTO.Resume;
            if (resume != null)
            {
                var fiveMB = 5 * 1024 * 1024;
                var pdfMimeType = "application/pdf";

                if (resume.Length > fiveMB || resume.ContentType != pdfMimeType)
                {
                    return BadRequest("Invalid file type or size.");
                }

                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "resumes");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Delete old file if exists
                if (!string.IsNullOrEmpty(existingCandidate.ResumeUrl))
                {
                    var oldFilePath = Path.Combine(directoryPath, existingCandidate.ResumeUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // Save new file
                var resumeURL = Guid.NewGuid().ToString() + ".pdf";
                var filePath = Path.Combine(directoryPath, resumeURL);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resume.CopyToAsync(stream);
                }

                existingCandidate.ResumeUrl = resumeURL;
            }

            candidateUpdateDTO.Id = id; // Ensure the ID remains the same
            _mapper.Map(candidateUpdateDTO, existingCandidate); // Updates the existing entity
            var response = await _candidateRepository.UpdateAsync(existingCandidate);



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

        // Download Resume PDF
        [HttpGet("download/{resumeFileName}")]
        public IActionResult DownloadToPdfFile(string resumeFileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "documents", "resumes", resumeFileName);
            
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File Not Found.");
            }

            var pdfBytes = System.IO.File.ReadAllBytes(filePath);
            var file = File(pdfBytes, "application/pdf", resumeFileName);
            return file;
        }
    }
}
