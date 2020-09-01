using System;
using WebApi.Models.Accounts;

namespace WebApi.Models.Members
{
    public class ProjectMemberResponse
    {
        public int ProjectMemberId { get; set; }
        public int? AccountId { get; set; }
        public AccountResponse Account { get; set; }
        public ShortAccountResponse ShortAccount { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
