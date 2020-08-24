using System;

namespace WebApi.Entities
{
    public class Member
    {
        public virtual int MemberId { get; set; }
        public virtual Project Project { get; set; }
        public virtual int ProjectId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }
        public Member()
        {
            Created = DateTime.UtcNow;
        }
    }
}
