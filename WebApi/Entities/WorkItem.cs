﻿using System;

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
        public virtual Project Project { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual Topic Topic { get; set; }
        public WorkItem()
        {
            Created = DateTime.UtcNow;
        }

        //public int? TopicId { get; set; }
        //public Rate Rate { get; set; }
        //public int? RateId { get; set; }

    }
}
