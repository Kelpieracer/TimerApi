using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Topics
{
    public class CreateTopicRequest
    {
        [Required]
        public string Name { get; set; }
    }
}