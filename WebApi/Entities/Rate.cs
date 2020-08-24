using System;

namespace WebApi.Entities
{
    public class Rate
    {
        public virtual int RateId { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual DateTime? Started { get; set; }
        public virtual DateTime? Ended { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual int AccountId { get; set; }
        public Rate()
        {
            Created = DateTime.UtcNow;
        }

        //public Customer Customer { get; set; }
        //public int CustomerId { get; set; }
    }
}
