using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public int TopicId { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public int RateId { get; set; }
        public int ProjectId { get; set; }
        public int AccountId { get; set; }
    }
}
