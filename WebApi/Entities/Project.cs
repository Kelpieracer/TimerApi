using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Project
    {
        public virtual int ProjectId { get; set; }
        public virtual string Name { get; set; }
        public virtual List<WorkItem> WorkItems { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }

        public virtual List<ProjectMember> ProjectMembers { get; set; }
        //public List<TopicsForProject> TopicsForProject { get; set; }

        //public Customer Customer { get; set; }
        //public int? CustomerId { get; set; }
        public Project()
        {
            Created = DateTime.UtcNow;
        }
    }
}
