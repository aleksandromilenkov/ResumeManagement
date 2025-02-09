using ResumeManagementAPI.Enums;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.DTOs.CompanyDTOs
{
    public class CompanyDTO : BaseEntity
    {
        public string Name { get; set; }
        public string Size { get; set; }
    }
}
