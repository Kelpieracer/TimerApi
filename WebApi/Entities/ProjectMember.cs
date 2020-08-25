using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class ProjectMember
    {
        public virtual int ProjectMemberId { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }

        [ForeignKey("AccountForeignKey")]
        public virtual int AccountId { get; set; }
        public ProjectMember()
        {
            Created = DateTime.UtcNow;
        }
    }
}
