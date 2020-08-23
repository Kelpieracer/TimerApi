using System;

namespace WebApi.Models.Rates
{
    public class RateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Started { get; set; }
        public DateTime Ended { get; set; }
    }
}
