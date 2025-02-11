using System.ComponentModel.DataAnnotations.Schema;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.DTOs.CandidateDTOs
{
    public class CandidateDTO : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CoverLetter { get; set; }
        public string ResumeUrl { get; set; }
        public string JobTitle { get; set; }
        public int JobId { get; set; }
    }
}
