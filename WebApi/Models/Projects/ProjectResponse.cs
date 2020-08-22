using System.Collections.Generic;
using WebApi.Models.Accounts;
using WebApi.Models.WorkItems;
using WebApi.Models.Rates;
using WebApi.Models.Customers;

namespace WebApi.Models.Projects
{
    public class ProjectResponse
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public List<WorkItemResponse> WorkItems { get; set; }
        public List<RateResponse> Rates { get; set; }
        public AccountResponse Manager { get; set; }
        public List<AccountResponse> Members { get; set; }
    }
}
