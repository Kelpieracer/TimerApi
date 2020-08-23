using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Topics
{
    public class CreateTopicRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}