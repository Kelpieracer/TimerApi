using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Models.Projects
{
    public class CreateProjectRequest
    {
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Account Manager { get; set; }
        public Customer Customer { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public ICollection<Account> Members { get; set; }
    }
}