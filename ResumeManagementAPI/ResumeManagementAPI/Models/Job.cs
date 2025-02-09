using System.ComponentModel.DataAnnotations.Schema;
using ResumeManagementAPI.Enums;

namespace ResumeManagementAPI.Models
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        public LevelEnum Level { get; set; }
    }
}
