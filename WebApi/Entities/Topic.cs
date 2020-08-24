using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Topic
    {
        public virtual int TopicId { get; set; }
        public virtual string Name { get; set; }
        //publivirtual c List<WorkItem> WorkItems { get; set; }
        public virtual List<TopicsForProject> TopicsForProjects { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }
        public Topic()
        {
            Created = DateTime.UtcNow;
        }
    }
}
