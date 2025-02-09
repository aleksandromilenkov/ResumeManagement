using ResumeManagementAPI.Enums;

namespace ResumeManagementAPI.Models
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public SizeEnum Size { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
