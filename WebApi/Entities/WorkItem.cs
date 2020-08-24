using System;

namespace WebApi.Entities
{
    public class WorkItem
    {
        public virtual int WorkItemId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? Started { get; set; }
        public virtual DateTime? Ended { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }
        public WorkItem()
        {
            Created = DateTime.UtcNow;
        }

        //public Topic Topic { get; set; }
        //public int? TopicId { get; set; }
        //public Rate Rate { get; set; }
        //public int? RateId { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
