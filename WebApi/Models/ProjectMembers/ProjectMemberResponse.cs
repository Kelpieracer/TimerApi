using WebApi.Entities;

namespace WebApi.Models.Members
{
    public class ProjectMemberResponse : CommonResponses
    {
        public int ProjectMemberId { get; set; }
        public Account Account { get; set; }
        public int ProjectId { get; set; }
    }
}
