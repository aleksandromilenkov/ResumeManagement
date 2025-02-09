using ResumeManagementAPI.Enums;

namespace ResumeManagementAPI.DTOs.JobDTOs
{
    public class JobCreateDTO
    {
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public string Level { get; set; }
    }
}
