using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Members
{
    public class CreateProjectMemberRequest
    {
        [Required]
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
    }
}