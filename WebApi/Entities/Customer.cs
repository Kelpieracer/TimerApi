using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Customer
    {
        public virtual int CustomerId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }
        public Customer()
        {
            Created = DateTime.UtcNow;
        }

        public virtual string Name { get; set; }
        public virtual List<Rate> Rates { get; set; }
        public virtual List<Project> Projects { get; set; }
    }
}
