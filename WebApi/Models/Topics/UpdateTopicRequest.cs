using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Topics
{
    public class UpdateTopicRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
