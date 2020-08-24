using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Topics
{
    public class UpdateTopicRequest
    {
        [Required]
        public int TopicId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
