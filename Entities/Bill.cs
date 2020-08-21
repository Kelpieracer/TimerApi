using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
        public Customer Customer { get; set; }
        public DateTime? Billed { get; set; }
        public DateTime? Paid { get; set; }
    }
}
