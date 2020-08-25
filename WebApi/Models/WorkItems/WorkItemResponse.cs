using System;
using WebApi.Entities;
using WebApi.Models;
using WebApi.Models.Topics;

namespace WebApi.Models.WorkItems
{
    public class WorkItemResponse : CommonResponses
    {
        public int WorkItemId { get; set; }
        public string Name { get; set; }
        public int? TopicTopicId { get; set; }
        public string TopicName { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public int? RateId { get; set; }
        public int? ProjectId { get; set; }
    }
}
