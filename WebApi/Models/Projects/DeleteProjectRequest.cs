using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Projects
{
    public class DeleteProjectRequest
    {
        [Required]
        public int ProjectId { get; set; }
    }
}