using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Members
{
    public class UpdateProjectMemberRequest
    {
        [Required]
        public int ProjectMemberId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public int AccountId { get; set; }
    }
}
