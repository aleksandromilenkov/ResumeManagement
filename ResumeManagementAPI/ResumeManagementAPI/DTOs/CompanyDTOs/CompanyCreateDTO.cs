using ResumeManagementAPI.Enums;

namespace ResumeManagementAPI.DTOs.CompanyDTOs
{
    public class CompanyCreateDTO
    {
        public string Name { get; set; }
        public SizeEnum Size { get; set; }
    }
}
