using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Bill
    {
        public virtual int BillId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }
        public Bill()
        {
            Created = DateTime.UtcNow;
        }

        public virtual DateTime? Billed { get; set; }
        public virtual DateTime? Paid { get; set; }
        public virtual List<WorkItem> WorkItems { get; set; }
        public virtual int? CustomerId { get; set; }
    }
}
