using ResumeManagementAPI.Enums;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.DTOs.CompanyDTOs
{
    public class CompanyDTO : BaseEntity
    {
        public string Name { get; set; }
        public SizeEnum Size { get; set; }
    }
}
