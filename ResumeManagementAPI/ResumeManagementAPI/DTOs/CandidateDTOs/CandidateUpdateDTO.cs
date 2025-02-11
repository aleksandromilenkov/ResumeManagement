namespace ResumeManagementAPI.DTOs.CandidateDTOs
{
    public class CandidateUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CoverLetter { get; set; }
        public IFormFile? Resume { get; set; }
        public int JobId { get; set; }
    }
}
