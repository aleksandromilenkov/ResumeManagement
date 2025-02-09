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
            CreateMap<Candidate, CandidateDTO>().ReverseMap();
            CreateMap<Candidate, CandidateCreateDTO>().ReverseMap();
            CreateMap<Company, CompanyDTO>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size.ToString()));
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();
            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level.ToString()));
            CreateMap<Job, JobCreateDTO>().ReverseMap();



        }
    }
}
