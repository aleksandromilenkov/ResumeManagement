using ResumeManagementAPI.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.DTOs.JobDTOs
{
    public class JobDTO : BaseEntity
    {
        public string Title { get; set; }
        public int CompanyId { get; set; }
        public string Level { get; set; }
    }
}
