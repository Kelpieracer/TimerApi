using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Topics
{
    public class DeleteTopicRequest
    {
        [Required]
        public int Id { get; set; }
    }
}