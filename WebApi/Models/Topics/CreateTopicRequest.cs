using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Topics
{
    public class CreateTopicRequest
    {
        [Required]
        public string Name { get; set; }
    }
    public class DeleteTopicRequest
    {
        [Required]
        public int Id { get; set; }
    }
}