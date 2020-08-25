using System;
using WebApi.Entities;
using WebApi.Models.Accounts;

namespace WebApi.Models.Members
{
    public class ProjectMemberResponse
    {
        public int ProjectMemberId { get; set; }
        public AccountResponse AccountResponse { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
