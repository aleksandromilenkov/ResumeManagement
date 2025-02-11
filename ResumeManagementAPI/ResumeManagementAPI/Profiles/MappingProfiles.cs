using AutoMapper;
using ResumeManagementAPI.DTOs.CandidateDTOs;
using ResumeManagementAPI.DTOs.CompanyDTOs;
using ResumeManagementAPI.DTOs.JobDTOs;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Candidate, CandidateDTO>().ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));
            CreateMap<CandidateDTO, Candidate>();
            CreateMap<Candidate, CandidateCreateDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
            CreateMap<JobDTO, Job>();
            CreateMap<Job, JobCreateDTO>().ReverseMap();



        }
    }
}
