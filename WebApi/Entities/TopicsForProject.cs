using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class TopicsForProject
    {
        [Key]
        public virtual int TopicsForProjectId { get; set; }
        public virtual Project Project { get; set; }
        //public virtual int ProjectId { get; set; }
        public virtual Topic Topic { get; set; }
        //public virtual int TopicId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual Account Account { get; set; }
        public TopicsForProject()
        {
            Created = DateTime.UtcNow;
        }
    }
}
