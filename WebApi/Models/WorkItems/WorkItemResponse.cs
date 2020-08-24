﻿using System;
using WebApi.Models;
namespace WebApi.Models.WorkItems
{
    public class WorkItemResponse : CommonResponses
    {
        public int WorkItemId { get; set; }
        public string Name { get; set; }
        public int? TopicId { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public int? RateId { get; set; }
        public int? ProjectId { get; set; }
    }
}
