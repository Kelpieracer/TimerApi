using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public Customer Customer { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public ICollection<Rate> Rates { get; set; }
        public Account Manager { get; set; }
        public ICollection<Account> Members { get; set; }
    }
}
