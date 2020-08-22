using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class WorkItem
    {
        public int WorkItemId { get; set; }
        public string Details { get; set; }
        public Topic Topics { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public Rate Rate { get; set; }
        public Project Project { get; set; }
        public Account Worker { get; set; }
    }
}
