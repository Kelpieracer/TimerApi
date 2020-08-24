using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Projects
{
    public class UpdateProjectRequest
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? CustomerId { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Account> Members { get; set; }
    }
}