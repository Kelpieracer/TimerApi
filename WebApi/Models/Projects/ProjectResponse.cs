using System.Collections.Generic;
using WebApi.Models.Members;
using WebApi.Models.Rates;
using WebApi.Models.WorkItems;

namespace WebApi.Models.Projects
{
    public class ProjectResponse : CommonResponses
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int? CustomerId { get; set; }
        public List<WorkItemResponse> WorkItems { get; set; }
        public List<ProjectMemberResponse> ProjectMembers { get; set; }
        public List<RateResponse> Rates { get; set; }
    }
}
