using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.WorkItems
{
    public class CreateWorkItemRequest
    {
        [Required]
        public string Name { get; set; }
        public int? TopicId { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public int? RateId { get; set; }
        public int? ProjectId { get; set; }
    }
}