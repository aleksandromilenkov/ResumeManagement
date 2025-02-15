﻿using System.Drawing;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ResumeManagementAPI.DTOs;
using ResumeManagementAPI.DTOs.CompanyDTOs;
using ResumeManagementAPI.Enums;
using ResumeManagementAPI.Interface;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // GET: api/company
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyRepository.GetAllAsync();
            var companiesDTOs = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return Ok(companiesDTOs);
        }

        // GET: api/company/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyRepository.FindByIdAsync(id);
            if (company == null)
                return NotFound(new { message = "Company not found." });
            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return Ok(companyDTO);
        }

        // POST: api/company
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompanyCreateDTO companyCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map the DTO to the Company entity
            var company = _mapper.Map<Company>(companyCreateDTO);
            var response = await _companyRepository.CreateAsync(company);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }

        // PUT: api/company/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CompanyDTO companyDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCompany = await _companyRepository.FindByIdAsync(id);
            if (existingCompany == null)
                return NotFound(new { message = "Company not found." });

            companyDTO.Id = id; // Ensure the ID remains the same
            var company = _mapper.Map<Company>(companyDTO);

            var response = await _companyRepository.UpdateAsync(company);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }

        // DELETE: api/company/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyRepository.FindByIdAsync(id);
            if (company == null)
                return NotFound(new { message = "Company not found." });

            var response = await _companyRepository.DeleteAsync(company);

            if (response.Flag)
                return Ok(response);

            return BadRequest(new { message = response.Message });
        }
    }
}
