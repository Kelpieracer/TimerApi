using System.Collections.Generic;

namespace WebApi.Models.Projects
{
    public class ReadProjectRequest
    {
        public List<int> ProjectIds { get; set; }
        public string NameContains { get; set; }
        public int? CustomerId { get; set; }
        public bool? FetchFullTree { get; set; }
    }
}